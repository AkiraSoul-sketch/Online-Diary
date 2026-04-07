function addOrUpdateCookie(key: string, value: string, maxAge: number): void {
  document.cookie = `${key}=${value}; path=/; domain=localhost;  max-age=${maxAge}; samesite=strict;`;
}

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

function deleteCookie(key: string): void {
  addOrUpdateCookie(key, "", -1);
}

function getCookieValue(key: string): string | null {
  const cookies: Record<string, string> = getCookiesDictionary();
  return cookies[key] || null;
}

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

const cookieLogic = () => {
  return { addOrUpdateCookie, getJsonCookie, getCookieValue, deleteCookie };
};

export { cookieLogic };
