import type { Grade } from "./gradebook.models";

function useGradebookLogic() {
  function resolveGradebookColor(grade: Grade): string {
    switch (grade.gradeValue) {
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

  return { resolveGradebookColor };
}

export { useGradebookLogic };
