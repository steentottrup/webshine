using System;
using System.Linq;
using WebShine.Core.Abstracts;

namespace WebShine.Core {

	public static class TemplateExtensions {

		public static Boolean HasSortOrderField(this ITemplate template) {
			return template.GetSortOrderField() != null;
		}

		public static Boolean HasAuthorField(this ITemplate template) {
			return template.GetAuthorField() != null;
		}

		public static Boolean HasEditorField(this ITemplate template) {
			return template.GetEditorField() != null;
		}

		public static IField GetSortOrderField(this ITemplate template) {
			IField field = template.Tabs.SelectMany(t => t.Fields).FirstOrDefault(f => f.Name == SystemFieldNames.SortOrder);
			return field == null ? null : field.IsValidSystemSortOrderField() ? field : null;
		}

		public static IField GetAuthorField(this ITemplate template) {
			IField field = template.Tabs.SelectMany(t => t.Fields).FirstOrDefault(f => f.Name == SystemFieldNames.Author);
			return field == null ? null : field.IsValidSystemAuthorField() ? field : null;
		}

		public static IField GetEditorField(this ITemplate template) {
			IField field = template.Tabs.SelectMany(t => t.Fields).FirstOrDefault(f => f.Name == SystemFieldNames.Editor);
			return field == null ? null : field.IsValidSystemEditorField() ? field : null;
		}
	}
}