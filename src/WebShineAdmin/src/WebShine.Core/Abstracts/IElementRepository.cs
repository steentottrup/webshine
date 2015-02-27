using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace WebShine.Core.Abstracts {

	public interface IElementRepository {
		// Element methods
		IElement Create(String template, String name, ObjectId parentId);
		IElement Read(ObjectId id);
		IElement Update(ObjectId id, IDictionary<String, Object> values);
		void Delete(ObjectId id);
		// TODO: Move to element tree!!!
		IEnumerable<IElement> ReadTreeLevel(ObjectId parentId);
		void MoveNode(ObjectId id, ObjectId newParentId, Int32 index);
		void MoveNode(ObjectId id, Int32 newIndex);
	}
}