use uuid::{Uuid};
use chrono::{DateTime, Utc};

struct Subject {
    id: Uuid,
    login: String,
    email: String,
    password_hash: String,
    is_archived: bool,
    email_confirmed: bool,
    created_at: DateTime<Utc>,
    updated_at: DateTime<Utc>,
    deleted_at: DateTime<Utc>,
}

impl Subject {
    
}