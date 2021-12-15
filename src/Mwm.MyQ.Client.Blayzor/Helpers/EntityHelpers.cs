using Mwm.MyQ.Domain;

namespace Mwm.MyQ.Client.Blayzor.Helpers {
    public static class EntityHelpers {

        public static string ToExternalLink(Initiative initiative, Project project) {
            var externalId = ToExternalId(initiative, project);
            return !string.IsNullOrEmpty(externalId) ? $"{project.ExternalAppBaseUrl}{externalId}" : string.Empty;
        }

        public static string ToExternalId(Initiative initiative, Project project) {
            if (project != null &&
                project.ExternalIdPrexfix != null &&
                project.ExternalAppBaseUrl != null &&
                initiative.ExternalId != null)
                return $"{project.ExternalIdPrexfix}-{initiative.ExternalId}";
            return string.Empty;
        }
    }
}
