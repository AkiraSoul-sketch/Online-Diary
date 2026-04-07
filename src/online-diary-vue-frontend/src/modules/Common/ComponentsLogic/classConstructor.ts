/**
 * Конструктор классов для объединения CSS классов в строку.
 *
 * @param {...Array<string | string[]>} classes - Список классов или массивов классов,
 * которые необходимо объединить. Принимает как отдельные строки, так и массивы строк.
 *
 * @returns {string} Строка, содержащая все переданные классы, разделённые пробелом.
 *
 * @example
 * // Передача отдельных строк
 * classConstructor('foo', 'bar', 'baz'); // 'foo bar baz'
 *
 * @example
 * // Передача массивов строк
 * classConstructor(['foo', 'bar'], ['baz']); // 'foo bar baz'
 *
 * @example
 * // Смешанная передача строк и массивов строк
 * classConstructor('foo', ['bar', 'baz']); // 'foo bar baz'
 */
export function classConstructor(...classes: Array<string | string[]>): string {
  return classes.flat().join(" ");
}
