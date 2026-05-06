pub trait Measurable<T = Self> {
    fn greater(self, other: T) -> bool;
    fn lesser(self, other: T) -> bool;
}
