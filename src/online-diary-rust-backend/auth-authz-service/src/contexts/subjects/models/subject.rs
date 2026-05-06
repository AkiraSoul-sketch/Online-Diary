use super::{Session, SessionId};
use chrono::{DateTime, Utc};
use shared_context::value_objects::Email;
use shared_context::value_objects::EntityLifetime;
use std::str::FromStr;
use uuid::Uuid;

#[derive(Clone)]
pub struct SubjectId {
    value: Uuid,
}

pub struct Subject {
    id: SubjectId,
    login: String,
    email: Email,
    password_hash: String,
    is_archived: bool,
    email_confirmed: bool,
    lifetime: EntityLifetime,
}

pub struct Snapshot {
    id: Uuid,
    login: String,
    email: String,
    password_hash: String,
    is_archived: bool,
    email_confirmed: bool,
    created_at: DateTime<Utc>,
    updated_at: Option<DateTime<Utc>>,
    deleted_at: Option<DateTime<Utc>>,
}

pub enum CreateError {
    EmptyLogin,
    LoginExceesLength,
    InvalidId,
}

pub enum SessionInitError {
    CannotInitWhenArchived,
}

pub enum SessionRefreshError {
    CannotRefreshNotOwnedSession,
}

pub const MAX_LOGIN_LENGTH: usize = 254;

impl Subject {
    pub fn init_new(
        login: String,
        email: Email,
        password_hash: String,
        is_archived: bool,
    ) -> Result<Subject, CreateError> {
        let trimmed_login: String = login.trim().to_string();
        if trimmed_login.is_empty() {
            return Err(CreateError::EmptyLogin);
        }

        if trimmed_login.len() > MAX_LOGIN_LENGTH {
            return Err(CreateError::LoginExceesLength);
        }

        let email_confirmed: bool = false;
        let id: SubjectId = SubjectId::new();
        let lifetime: EntityLifetime = EntityLifetime::new();
        let result: Subject = Subject {
            id,
            login,
            email,
            password_hash,
            is_archived,
            email_confirmed,
            lifetime,
        };

        return Ok(result);
    }

    pub fn init_session(
        &self,
        refresh_token: String,
        expires_at: DateTime<Utc>,
    ) -> Result<Session, SessionInitError> {
        match &self.lifetime.access() {
            Err(_) => Err(SessionInitError::CannotInitWhenArchived),
            Ok(_) => {
                let result: Session = Session::new(self.id.clone(), refresh_token, expires_at);
                Ok(result)
            }
        }
    }

    pub fn refresh_session(
        &self,
        session: &mut Session,
        refresh_token: String,
        expires_at: DateTime<Utc>,
    ) -> Result<(), SessionRefreshError> {
        match self.owns_session(&session) {
            false => Err(SessionRefreshError::CannotRefreshNotOwnedSession),
            true => {
                session.refresh(refresh_token, expires_at);
                Ok(())
            }
        }
    }

    pub fn snapshot(&self) -> Snapshot {
        let lifetime_snapshot = self.lifetime.snapshot();
        return Snapshot {
            id: self.id.value,
            login: self.login.clone(),
            email: self.email.snapshot().value,
            password_hash: self.password_hash.clone(),
            is_archived: self.lifetime.is_archived(),
            email_confirmed: self.email_confirmed,
            created_at: lifetime_snapshot.created_at,
            updated_at: lifetime_snapshot.updated_at,
            deleted_at: lifetime_snapshot.deleted_at,
        };
    }

    fn owns_session(&self, session: &Session) -> bool {
        let snapshot = session.snapshot();
        return self.id.value == snapshot.subject_id;
    }
}

impl SubjectId {
    pub fn new() -> SubjectId {
        let value: Uuid = Uuid::new_v4();
        return Self { value };
    }

    pub fn from_string(value: String) -> Result<SubjectId, CreateError> {
        match Uuid::from_str(&value) {
            Err(_) => Err(CreateError::InvalidId),
            Ok(res) => Ok(SubjectId { value: res }),
        }
    }

    pub fn snapshot(&self) -> Uuid {
        return self.value;
    }
}
