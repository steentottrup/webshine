using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace WebShine.Core.Abstracts {

	public interface IElementTree {
		IEnumerable<IElement> ReadTreeLevel(ObjectId parentId);
		void MoveNode(ObjectId id, ObjectId newParentId, Int32 index);
		void MoveNode(ObjectId id, Int32 newIndex);
	}
}