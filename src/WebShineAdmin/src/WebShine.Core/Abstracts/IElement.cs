﻿using MongoDB.Bson;
using System;

namespace WebShine.Core.Abstracts {

	public interface IElement {
		ObjectId Id { get; set; }
		ObjectId ParentId { get; set; }
		String Name { get; set; }
		String Tree { get; set; }
		String Template { get; set; }
		IProperty[] Properties { get; set; }
	}
}