use crate::utilities::Measurable;
use chrono::{DateTime, Utc};

pub struct EntityLifetime {
    created_at: DateTime<Utc>,
    updated_at: Option<DateTime<Utc>>,
    deleted_at: Option<DateTime<Utc>>,
}

pub struct Snapshot {
    pub created_at: DateTime<Utc>,
    pub updated_at: Option<DateTime<Utc>>,
    pub deleted_at: Option<DateTime<Utc>>,
}

#[derive(Debug)]
pub enum CreateError {
    CreatedAtGreaterThanDeletedAt,
    CreatedAtGreaterThanUpdatedAt,
}

#[derive(Debug)]
pub enum ArchiveError {
    CannotArchiveAlreadyArchived,
}

#[derive(Debug)]
pub enum ActivateError {
    CannotActivateAlreadyActivated,
}

#[derive(Debug)]
pub enum AccessError {
    CannotAccessArchivedEntity,
}

impl EntityLifetime {
    pub fn archive(&mut self) -> Result<(), ArchiveError> {
        if self.is_archived() {
            return Err(ArchiveError::CannotArchiveAlreadyArchived);
        }

        let now: DateTime<Utc> = Utc::now();
        self.deleted_at = Some(now);
        self.updated_at = Some(now);
        Ok(())
    }

    pub fn activate(&mut self) -> Result<(), ActivateError> {
        if self.is_active() {
            return Err(ActivateError::CannotActivateAlreadyActivated);
        }

        self.deleted_at = None;
        self.update();
        Ok(())
    }

    pub fn is_active(&self) -> bool {
        self.deleted_at.is_none()
    }

    pub fn is_archived(&self) -> bool {
        self.deleted_at.is_some()
    }

    pub fn snapshot(&self) -> Snapshot {
        Snapshot {
            created_at: self.created_at,
            updated_at: self.updated_at,
            deleted_at: self.deleted_at,
        }
    }

    pub fn update(&mut self) {
        self.updated_at = Some(Utc::now());
    }

    pub fn can_access(&self) -> bool {
        self.is_active()
    }

    pub fn access(&self) -> Result<(), AccessError> {
        if self.is_archived() {
            return Err(AccessError::CannotAccessArchivedEntity);
        }

        Ok(())
    }

    pub fn new() -> EntityLifetime {
        let created_at: DateTime<Utc> = Utc::now();
        let updated_at: Option<DateTime<Utc>> = None;
        let deleted_at: Option<DateTime<Utc>> = None;

        EntityLifetime {
            created_at,
            updated_at,
            deleted_at,
        }
    }

    pub fn create(
        created_at: DateTime<Utc>,
        updated_at: Option<DateTime<Utc>>,
        deleted_at: Option<DateTime<Utc>>,
    ) -> Result<EntityLifetime, CreateError> {
        if created_at_greater_than_updated_at(created_at, updated_at) {
            return Err(CreateError::CreatedAtGreaterThanUpdatedAt);
        }

        if created_at_greater_than_deleted_at(created_at, deleted_at) {
            return Err(CreateError::CreatedAtGreaterThanDeletedAt);
        }

        let result: EntityLifetime = EntityLifetime {
            created_at,
            updated_at,
            deleted_at,
        };

        Ok(result)
    }
}

fn created_at_greater_than_updated_at(
    created_at: DateTime<Utc>,
    updated_at: Option<DateTime<Utc>>,
) -> bool {
    if updated_at.is_none() {
        return false;
    }

    let actual_updated_at: DateTime<Utc> = updated_at.unwrap();
    created_at.greater(actual_updated_at)
}

fn created_at_greater_than_deleted_at(
    created_at: DateTime<Utc>,
    deleted_at: Option<DateTime<Utc>>,
) -> bool {
    if deleted_at.is_none() {
        return false;
    }

    let actual_deleted_at: DateTime<Utc> = deleted_at.unwrap();
    created_at.greater(actual_deleted_at)
}
