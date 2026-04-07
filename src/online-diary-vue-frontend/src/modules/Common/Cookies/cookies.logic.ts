/**
 * Добавляет новый куки или обновляет существующий в браузере.
 *
 * @param {string} key - Ключ (название) куки, по которому он будет идентифицирован.
 * @param {string} value - Значение куки, которое необходимо сохранить.
 * @param {number} maxAge - Максимальное время жизни куки в секундах.
 *                          По истечении данного времени куки будет автоматически удалён.
 * @returns {void}
 *
 * @example
 * // Добавить куки с временем жизни 1 час
 * addOrUpdateCookie('token', 'abc123', 3600);
 */
function addOrUpdateCookie(key: string, value: string, maxAge: number): void {
  document.cookie = `${key}=${value}; path=/; domain=localhost;  max-age=${maxAge}; samesite=strict;`;
}

/**
 * Получает cookie по указанному ключу и пытается преобразовать её значение из JSON в объект заданного типа.
 *
 * @typeParam T - Тип данных, в который должно быть преобразовано значение cookie.
 * @param key - Ключ cookie, значение которой необходимо получить.
 * @returns Распарсенное значение cookie типа {@link T}, если cookie найдена и содержит корректный JSON;
 * в противном случае возвращает `null`.
 *
 * @remarks
 * Функция перебирает все cookie из `document.cookie`, ищет совпадение по имени и выполняет
 * `JSON.parse` для значения найденной cookie. Если разбор JSON завершится с ошибкой,
 * функция проигнорирует её и вернёт `null`.
 */
function getJsonCookie<T>(key: string): T | null {
  let value: T | null = null;
  document.cookie.split(";").forEach((cookie) => {
    const [cookieKey, cookieValue] = cookie.trim().split("=");
    if (cookieKey === key) {
      try {
        value = JSON.parse(cookieValue) as T;
        return;
      } catch {}
    }
  });
  return value;
}

/**
 * Удаляет cookie по указанному ключу, устанавливая для него пустое значение
 * и отрицательное время жизни, что приводит к немедленному истечению срока действия.
 *
 * @param key Ключ cookie, которую необходимо удалить.
 */
function deleteCookie(key: string): void {
  addOrUpdateCookie(key, "", -1);
}

/**
 * Возвращает значение cookie по указанному ключу.
 *
 * Функция получает словарь всех cookie через `getCookiesDictionary()`
 * и пытается найти значение по переданному имени.
 *
 * @param key - Имя cookie, значение которой нужно получить.
 * @returns Значение cookie, если ключ найден; иначе `null`.
 */
function getCookieValue(key: string): string | null {
  const cookies: Record<string, string> = getCookiesDictionary();
  return cookies[key] || null;
}

/**
 * Преобразует строку cookies браузера (`document.cookie`) в словарь вида
 * `ключ -> значение`.
 *
 * Функция:
 * - разбивает строку cookies по символу `;`;
 * - удаляет лишние пробелы у каждой пары;
 * - разделяет каждую запись по символу `=`;
 * - отбрасывает некорректные элементы, если в них отсутствует ключ или значение;
 * - собирает результат в объект.
 *
 * @returns Объект, содержащий cookies, где ключом является имя cookie,
 * а значением — её значение.
 */
function getCookiesDictionary(): Record<string, string> {
  return document.cookie
    .split(";")
    .map((cookie) => {
      return cookie.trim().split("=");
    })
    .filter((parts) => {
      if (parts.length !== 2) return false;
      const [key, value] = parts;
      return key && value;
    })
    .reduce<Record<string, string>>((acc, [key, value]) => {
      acc[key] = value;
      return acc;
    }, {});
}

/**
 * Создаёт и возвращает объект с методами для работы с cookie.
 *
 * @returns Объект с функциями управления cookie:
 * - `addOrUpdateCookie` — добавляет новую cookie или обновляет существующую;
 * - `getJsonCookie` — получает значение cookie и преобразует его из JSON;
 * - `getCookieValue` — возвращает строковое значение cookie;
 * - `deleteCookie` — удаляет cookie.
 */
const cookieLogic = () => {
  return { addOrUpdateCookie, getJsonCookie, getCookieValue, deleteCookie };
};

export { cookieLogic };
