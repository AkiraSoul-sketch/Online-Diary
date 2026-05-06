mod role;

pub use role::{Role, RoleId};

pub mod role_errors {
    pub use super::role::{CreateError, InvalidRoleId};
}

pub mod role_constants {
    pub use super::role::{MAX_NAME_LENGTH, MIN_NAME_LENGTH};
}
