interface AdminPanelMenuCategory {
  menuName: string;
  type: "left" | "right" | "item";
  items?: AdminPanelMenuCategoryItem[];
}

interface AdminPanelMenuCategoryItem {
  menuName: string;
  route: string;
}

export type { AdminPanelMenuCategory, AdminPanelMenuCategoryItem };
