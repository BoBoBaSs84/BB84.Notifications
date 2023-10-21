namespace BB84.Notifications.Enumerators;

/// <summary>
/// The collection event type enumerator.
/// </summary>
public enum CollectionEventType
{
  /// <summary>
  /// An item was added to the collection.
  /// </summary>
  Add,
  /// <summary>
  /// The content of the collection was cleared.
  /// </summary>
  Clear,
  /// <summary>
  /// An item was moved within the collection.
  /// </summary>
  Move,
  /// <summary>
  /// An item was removed from the collection.
  /// </summary>
  Remove,
  /// <summary>
  /// An item was replaced in the collection.
  /// </summary>
  Replace,
  /// <summary>
  /// An item was updated in the collection.
  /// </summary>
  Update
}
