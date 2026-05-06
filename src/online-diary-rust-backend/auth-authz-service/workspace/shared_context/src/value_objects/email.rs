use once_cell::sync::Lazy;
use regex::Regex;

static EMAIL_REGEX: Lazy<Regex> =
    Lazy::new(|| Regex::new(r"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$").unwrap());

pub struct Email {
    value: String,
}

pub struct Snapshot {
    pub value: String,
}

pub enum CreateError {
    InvalidEmail,
    EmptyEmail,
}

impl Email {
    pub fn snapshot(&self) -> Snapshot {
        Snapshot {
            value: self.value.clone(),
        }
    }

    pub fn create(value: String) -> Result<Email, CreateError> {
        let trimmed_value: String = value.trim().to_string();

        if String::is_empty(&trimmed_value) {
            return Err(CreateError::EmptyEmail);
        }

        match validate_email_format(&trimmed_value) {
            Err(error) => Err(error),
            Ok(()) => Ok(Email {
                value: trimmed_value,
            }),
        }
    }
}

fn validate_email_format(value: &str) -> Result<(), CreateError> {
    match EMAIL_REGEX.is_match(value) {
        true => Ok(()),
        false => Err(CreateError::InvalidEmail),
    }
}
