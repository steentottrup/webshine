using System;

namespace WebShine.Core.Abstracts {

	public static class ITextCarrierExtensions {

		public static String GetText(this ITextCarrier field, String language) {
			if (field.LocalizedTexts != null && (field.LocalizedTexts.ContainsKey(String.Empty) || field.LocalizedTexts.ContainsKey(language))) {
				return field.LocalizedTexts.ContainsKey(language) ? field.LocalizedTexts[language] : field.LocalizedTexts[String.Empty];
			}
			return String.Empty;
		}
	}
}