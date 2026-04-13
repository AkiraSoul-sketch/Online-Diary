export interface DisciplinesViewProps {
  toggleFiltersDrawer(): void;
  canRenderTable(): boolean;
  isFiltersDrawerOpen(): boolean;
  isCreateDrawerOpen(): boolean;
  toggleCreateDrawer(): void;
}
