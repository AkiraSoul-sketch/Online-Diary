import { useMediaQuery } from "@vueuse/core";
import type { ComputedRef } from "vue";

export class Components {
  public static HTMLElementByRef(
    component: any,
    refName: string,
  ): HTMLElement | null {
    if (!component || !refName) {
      return null;
    }
    const element: HTMLElement | null = component.$refs[
      refName
    ] as HTMLElement | null;
    return element;
  }

  public static DisposeResizeObserver(
    observer: ResizeObserver | null | undefined,
  ): void {
    if (observer === null) return;
    if (observer == undefined) return;
    observer.disconnect();
    observer = null;
  }

  public static CreateMediaQueryTracker(): MediaQueryTracker {
    const queries = this.GetScreenWidthMediaQueries();
    const mediaQueryTracker = new MediaQueryTracker(
      queries.isXs,
      queries.isSm,
      queries.isMd,
      queries.isLg,
      queries.isXl,
    );
    return mediaQueryTracker;
  }

  public static GetScreenWidthMediaQueries() {
    const isXs = this.IsXsScreen();
    const isSm = this.IsSmScreen();
    const isMd = this.IsMdScreen();
    const isLg = this.IsLgScreen();
    const isXl = this.IsXlScreen();
    return {
      isSm,
      isMd,
      isLg,
      isXl,
      isXs,
    };
  }

  public static IsMdScreen() {
    const isMd = useMediaQuery("(min-width: 768px) and (max-width: 1023px)");
    return isMd;
  }

  public static IsSmScreen() {
    const isSm = useMediaQuery("(min-width: 640px) and (max-width: 767px)");
    return isSm;
  }

  public static IsXsScreen() {
    const isXs = useMediaQuery("(max-width: 639px)");
    return isXs;
  }

  public static IsLgScreen() {
    const isLg = useMediaQuery("(min-width: 1024px) and (max-width: 1279px)");
    return isLg;
  }

  public static IsXlScreen() {
    const isXl = useMediaQuery("(min-width: 1280px)");
    return isXl;
  }
}

export class MediaQueryTracker {
  private readonly _isXs: ComputedRef<boolean>;
  private readonly _isSm: ComputedRef<boolean>;
  private readonly _isMd: ComputedRef<boolean>;
  private readonly _isLg: ComputedRef<boolean>;
  private readonly _isXl: ComputedRef<boolean>;
  constructor(
    isXs: ComputedRef<boolean>,
    isSm: ComputedRef<boolean>,
    isMd: ComputedRef<boolean>,
    isLg: ComputedRef<boolean>,
    isXl: ComputedRef<boolean>,
  ) {
    this._isXs = isXs;
    this._isSm = isSm;
    this._isMd = isMd;
    this._isLg = isLg;
    this._isXl = isXl;
  }

  get isXs() {
    return this._isXs.value;
  }

  get isSm() {
    return this._isSm.value;
  }

  get isMd() {
    return this._isMd.value;
  }

  get isLg() {
    return this._isLg.value;
  }

  get isXl() {
    return this._isXl.value;
  }
}
