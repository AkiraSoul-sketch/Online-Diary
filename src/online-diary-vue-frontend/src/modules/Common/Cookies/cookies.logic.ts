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

const cookieLogic = () => {
  return { addOrUpdateCookie, getJsonCookie };
};

export { cookieLogic };
