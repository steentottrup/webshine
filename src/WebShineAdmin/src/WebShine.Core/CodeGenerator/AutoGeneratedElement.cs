﻿using System;
using MongoDB.Bson;

namespace WebShine.Core.CodeGenerator {

	public abstract class AutoGeneratedElement {
		public ObjectId Id { get; set; }
		public ObjectId ParentId { get; set; }
	}
}