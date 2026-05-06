use crate::utilities::Comparable;
use crate::utilities::Measurable;
use chrono::{DateTime, Utc};

impl Comparable<DateTime<Utc>> for DateTime<Utc> {
    fn equals(self, other: DateTime<Utc>) -> bool {
        let timestamp_self: i64 = self.timestamp();
        let timestamp_other: i64 = other.timestamp();
        return timestamp_self == timestamp_other;
    }
}

impl Measurable<DateTime<Utc>> for DateTime<Utc> {
    fn greater(self, other: DateTime<Utc>) -> bool {
        let timestamp_self: i64 = self.timestamp();
        let timestamp_other: i64 = other.timestamp();
        return timestamp_self > timestamp_other;
    }

    fn lesser(self, other: DateTime<Utc>) -> bool {
        let timestamp_self: i64 = self.timestamp();
        let timestamp_other: i64 = other.timestamp();
        return timestamp_self < timestamp_other;
    }
}
