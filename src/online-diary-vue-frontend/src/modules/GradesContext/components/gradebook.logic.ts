import type { Grade } from "./gradebook.models";

const themeDateFormatter = new Intl.DateTimeFormat("ru-RU", {
  day: "2-digit",
  month: "2-digit",
});

function useGradebookLogic() {
  function resolveGradebookColorValue(gradeValue: string | null): string {
    switch (gradeValue) {
      case "2":
        return "hsl(20 100% 50% / 15%)";
      case "3":
        return "hsl(50 100% 50% / 15%)";
      case "4":
        return "hsl(80 100% 50% / 15%)";
      case "5":
        return "hsl(110 100% 50% / 15%)";
      case "У":
        return "hsl(170 100% 50% / 15%)";
      case "НА":
        return "hsl(0 100% 50% / 15%)";
      case "Н":
        return "hsl(0 10% 50% / 15%)";
      default:
        return "hsl(0 10% 50% / 15%)";
    }
  }

  function resolveGradebookColor(grade: Grade): string {
    return resolveGradebookColorValue(grade.gradeValue);
  }

  function formatThemeDate(date: Date): string {
    return themeDateFormatter.format(date);
  }

  return { resolveGradebookColor, resolveGradebookColorValue, formatThemeDate };
}

export { useGradebookLogic };
