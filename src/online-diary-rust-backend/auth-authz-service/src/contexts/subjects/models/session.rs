use std::str::FromStr;

use super::SubjectId;
use chrono::{DateTime, Utc};
use uuid::Uuid;

pub struct SessionId {
    value: Uuid,
}

pub struct Session {
    id: SessionId,
    subject_id: SubjectId,
    refresh_token: String,
    refresh_token_expires_at: DateTime<Utc>,
}

pub struct Snapshot {
    pub id: Uuid,
    pub subject_id: Uuid,
    pub refresh_token: String,
    pub refresh_token_expires_at: DateTime<Utc>,
}

pub enum CreateError {
    InvalidSessionId,
}

impl Session {
    pub fn new(subject_id: SubjectId, refresh_token: String, expires_at: DateTime<Utc>) -> Session {
        let session_id: SessionId = SessionId::new();
        return {
            Session {
                id: session_id,
                subject_id,
                refresh_token,
                refresh_token_expires_at: expires_at,
            }
        };
    }

    pub fn from(
        session_id: SessionId,
        subject_id: SubjectId,
        refresh_token: String,
        expires_at: DateTime<Utc>,
    ) -> Session {
        return Self {
            id: session_id,
            subject_id,
            refresh_token,
            refresh_token_expires_at: expires_at,
        };
    }

    pub fn refresh(
        &mut self,
        refresh_token: String,
        refresh_token_expires_at: DateTime<Utc>,
    ) -> () {
        self.refresh_token = refresh_token;
        self.refresh_token_expires_at = refresh_token_expires_at;
    }

    pub fn snapshot(&self) -> Snapshot {
        return Snapshot {
            id: self.id.value,
            refresh_token: self.refresh_token.clone(),
            refresh_token_expires_at: self.refresh_token_expires_at,
            subject_id: self.subject_id.snapshot(),
        };
    }
}

impl SessionId {
    pub fn new() -> SessionId {
        return SessionId {
            value: Uuid::new_v4(),
        };
    }

    pub fn from(value: String) -> Result<SessionId, CreateError> {
        match Uuid::from_str(&value) {
            Err(_) => Err(CreateError::InvalidSessionId),
            Ok(id) => Ok(SessionId { value: id }),
        }
    }
}
