mod email;
mod entity_lifetime;

pub use email::Email;
pub mod email_errors {
    pub use super::email::CreateError;
}

pub use entity_lifetime::{EntityLifetime, Snapshot as EntityLifeTimeSnapshot};
pub mod entity_lifetime_errors {
    pub use super::entity_lifetime::{AccessError, ActivateError, ArchiveError, CreateError};
}
