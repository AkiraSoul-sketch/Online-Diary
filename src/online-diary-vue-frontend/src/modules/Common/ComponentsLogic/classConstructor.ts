export function classConstructor(...classes: Array<string | string[]>): string {
  return classes.flat().join(" ");
}
