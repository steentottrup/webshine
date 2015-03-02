using System;
using WebShine.Core.Abstracts;

namespace WebShine.Core {

	public static class FieldExtensions {

		public static Boolean IsValidSystemSortOrderField(this IField field) {
			return field.Name == SystemFieldNames.SortOrder &&
				field.DataType == SystemDataTypes.Int32 &&
				field.Required == true;
		}

		public static Boolean IsValidSystemAuthorField(this IField field) {
			return field.Name == SystemFieldNames.Author &&
				IsValidSystemUserField(field);
		}

		public static Boolean IsValidSystemEditorField(this IField field) {
			return field.Name == SystemFieldNames.Editor &&
				IsValidSystemUserField(field);
		}

		public static Boolean IsValidSystemChangedField(this IField field) {
			return field.Name == SystemFieldNames.Changed &&
				IsValidSystemDatesteampField(field);
		}

		public static Boolean IsValidSystemCreatedField(this IField field) {
			return field.Name == SystemFieldNames.Created &&
				IsValidSystemDatesteampField(field);
		}

		private static Boolean IsValidSystemUserField(IField field) {
			return field.DataType == SystemDataTypes.ObjectId &&
				field.Editable == false &&
				field.Required == true;
		}

		private static Boolean IsValidSystemDatesteampField(IField field) {
			return field.DataType == SystemDataTypes.DateTime &&
				field.Editable == false &&
				field.Required == true;
		}
	}
}