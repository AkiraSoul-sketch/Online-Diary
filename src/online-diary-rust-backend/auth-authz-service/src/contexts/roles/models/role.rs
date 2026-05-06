use chrono::{DateTime, Utc};
use shared_context::value_objects::EntityLifetime;
use std::str::FromStr;
use uuid::Uuid;

#[derive(Clone, PartialEq, Eq)]
pub struct RoleId {
    uuid: Uuid,
}

pub enum InvalidRoleId {
    InvalidUuid(uuid::Error),
}

impl RoleId {
    pub fn new() -> RoleId {
        RoleId {
            uuid: Uuid::new_v4(),
        }
    }

    pub fn from_string(s: &str) -> Result<RoleId, InvalidRoleId> {
        return match Uuid::from_str(s) {
            Ok(uuid) => Ok(RoleId { uuid }),
            Err(err) => Err(InvalidRoleId::InvalidUuid(err)),
        };
    }

    pub fn snapshot(&self) -> Uuid {
        return self.uuid;
    }
}

pub struct Role {
    id: RoleId,
    name: String,
    lifetime: EntityLifetime,
}

pub struct Snapshot {
    id: Uuid,
    name: String,
    created_at: DateTime<Utc>,
    updated_at: Option<DateTime<Utc>>,
    deleted_at: Option<DateTime<Utc>>,
    is_archived: bool,
}

pub enum CreateError {
    NameEmpty,
    NameTooLong,
    NameTooShort,
}

pub const MIN_NAME_LENGTH: usize = 3;
pub const MAX_NAME_LENGTH: usize = 255;

impl Role {
    pub fn new(name: String, lifetime: EntityLifetime) -> Result<Role, CreateError> {
        let id: RoleId = RoleId::new();
        return Role::from(id, name, lifetime);
    }

    pub fn from(id: RoleId, name: String, lifetime: EntityLifetime) -> Result<Role, CreateError> {
        let trimmed_name = name.trim();
        if trimmed_name.is_empty() {
            return Err(CreateError::NameEmpty);
        }

        if trimmed_name.len() < MIN_NAME_LENGTH {
            return Err(CreateError::NameTooShort);
        }
        if trimmed_name.len() > MAX_NAME_LENGTH {
            return Err(CreateError::NameTooLong);
        }

        return Ok(Role {
            id,
            name: trimmed_name.to_string(),
            lifetime,
        });
    }

    pub fn snapshot(&self) -> Snapshot {
        let lifetime_snapshot = self.lifetime.snapshot();
        Snapshot {
            id: self.id.snapshot(),
            name: self.name.clone(),
            created_at: lifetime_snapshot.created_at,
            updated_at: lifetime_snapshot.updated_at,
            deleted_at: lifetime_snapshot.deleted_at,
            is_archived: self.lifetime.is_archived(),
        }
    }
}
