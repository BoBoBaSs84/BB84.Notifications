[![CI](https://github.com/BoBoBaSs84/BB84.Notifications/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/BoBoBaSs84/BB84.Notifications/actions/workflows/ci.yml)
[![Docs](https://github.com/BoBoBaSs84/BB84.Notifications/actions/workflows/docs.yml/badge.svg?branch=main)](https://github.com/BoBoBaSs84/BB84.Notifications/actions/workflows/docs.yml)
[![CodeQL](https://github.com/BoBoBaSs84/BB84.Notifications/actions/workflows/codeql.yml/badge.svg?branch=main)](https://github.com/BoBoBaSs84/BB84.Notifications/actions/workflows/codeql.yml)
[![Issues](https://img.shields.io/github/issues/BoBoBaSs84/BB84.Notifications)](https://github.com/BoBoBaSs84/BB84.Notifications/issues)
[![Commit](https://img.shields.io/github/last-commit/BoBoBaSs84/BB84.Notifications)](https://github.com/BoBoBaSs84/BB84.Notifications/commit/main)
[![License](https://img.shields.io/github/license/BoBoBaSs84/BB84.Notifications)](https://github.com/BoBoBaSs84/BB84.Notifications/blob/main/LICENSE)
[![NuGet](https://img.shields.io/nuget/v/BB84.Notifications.svg?logo=nuget&logoColor=white)](https://www.nuget.org/packages/BB84.Notifications)

# BB84.Notifications

Contains relevant things for property one-way binding / two-way binding, for property change / changing notification and for collection change / changing notification.

## Usage

Depending on the application, there are several ways to skin a cat.

### The bindable property class and interfaces

Designed with one or two way binding in mind.

```csharp
public IBindableProperty<int> BindableInt { get; set; } = new BindableProperty<int>(default);
```

If the property is changed, the `PropertyChangingEventHandler` is triggered before the value changes and the `PropertyChangedEventHandler` is triggered after the value has changed.

- The `PropertyChangingEventHandler` contains via the `PropertyChangingEventArgs` the name of the property that is changing and when casted to `PropertyChangingEventArgs<T>` the old value of the property.
- The `PropertyChangedEventHandler` contains via the `PropertyChangedEventArgs` the name of the property that has changed and when casted to `PropertyChangedEventArgs<T>` the new value of the property.

Further implementation possibilities can be achieved with the help of the `IBindableProperty` interface.

### The notification property base class and interfaces

Designed to handle changes at the class level and propagate them forward to the outside.

```csharp
public sealed class TestClass : NotifyPropertyBase
{
    private int _property;

    public int Property
    {
        get => _property;
        set => SetProperty(ref _property, value);
    }
}
```

If the property is changed, the `PropertyChangingEventHandler` is triggered before the value changes and the `PropertyChangedEventHandler` is triggered after the value has changed.

- The `PropertyChangingEventHandler` contains via the `PropertyChangingEventArgs` the name of the property that is changing and when casted to `PropertyChangingEventArgs<T>` the old value of the property.
- The `PropertyChangedEventHandler` contains via the `PropertyChangedEventArgs` the name of the property that has changed and when casted to `PropertyChangedEventArgs<T>` the new value of the property.

Further implementation possibilities can be achieved with the help of the `INotifyPropertyBase` interface.

### The notification collection base class and interfaces

The `NotifyCollectionBase` class provides methods to handle the change within a collection and propagate it to the outside.

```csharp
public sealed class MyStringCollection : NotifyCollectionBase, ICollection<string>
{
  private readonly Collection<string> _collection;

  public MyStringCollection()
    => _collection = new Collection<string>();

  ..
}
```

Most of the implementation must be done despite the base class itself. The intention has been to be able to signal the state of a collection before (`CollectionChangingEventHandler`) or after (`CollectionChangedEventHandler`) the addition, deletion or modification of objects.

- The `CollectionChangingEventHandler` contains via the `CollectionChangingEventArgs` the `CollectionChangeAction` and when casted to `CollectionChangingEventArgs<T>` the old value of the object.
- The `CollectionChangedEventHandler` contains via the `CollectionChangedEventArgs` the `CollectionChangeAction` and when casted to `CollectionChangedEventArgs<T>` the new value of the object.

Further implementation possibilities can be achieved with the interfaces `INotifyCollectionChanged` and `INotifyCollectionChanging`.

### The notification attributes

The `NotifyChangedAttribute` and the `NotifyChangingAttribute` propagating the information also to those properties the are defined via the constructor. An example implementation could look like this:

```csharp
private sealed class TestClass : NotifyPropertyBase
{
  private int _quantity;
  private int _value;

  public TestClass(int quantity, int value)
  {
    Quantity = quantity;
    Value = value;
  }

  [NotifyChanged(nameof(TotalValue))]
  public int Quantity { get => _quantity; set => SetProperty(ref _quantity, value); }

  [NotifyChanged(nameof(TotalValue))]
  public int Value { get => _value; set => SetProperty(ref _value, value); }

  public int TotalValue => Quantity * Value;
}
```

If the `Quantity` property or the `Value` property is changed, a `PropertyChanged` event is also triggered for the `TotalValue` property.

## Documentation

The complete API documentation can be found [here](https://bobobass84.github.io/BB84.Notifications/).
