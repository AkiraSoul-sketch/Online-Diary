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
}
