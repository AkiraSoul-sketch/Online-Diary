pub trait Comparable<T = Self> {
    fn equals(self, other: T) -> bool;
}
