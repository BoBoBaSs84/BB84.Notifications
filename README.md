[![CI](https://github.com/BoBoBaSs84/BB84.Notifications/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/BoBoBaSs84/BB84.Notifications/actions/workflows/ci.yml)
[![CD](https://github.com/BoBoBaSs84/BB84.Notifications/actions/workflows/cd.yml/badge.svg?branch=main)](https://github.com/BoBoBaSs84/BB84.Notifications/actions/workflows/cd.yml)
[![C#](https://img.shields.io/badge/C%23-13.0-239120)](https://github.com/BoBoBaSs84/BB84.Notifications)
[![Issues](https://img.shields.io/github/issues/BoBoBaSs84/BB84.Notifications)](https://github.com/BoBoBaSs84/BB84.Notifications/issues)
[![Commit](https://img.shields.io/github/last-commit/BoBoBaSs84/BB84.Notifications)](https://github.com/BoBoBaSs84/BB84.Notifications/commit/main)
[![License](https://img.shields.io/github/license/BoBoBaSs84/BB84.Notifications)](https://github.com/BoBoBaSs84/BB84.Notifications/blob/main/LICENSE)
[![NuGet](https://img.shields.io/nuget/v/BB84.Notifications.svg?logo=nuget&logoColor=white)](https://www.nuget.org/packages/BB84.Notifications)

[![net20](https://img.shields.io/badge/netstandard2.0-5C2D91?logo=.NET&labelColor=gray)](https://github.com/BoBoBaSs84/BB84.Notifications)
[![net462](https://img.shields.io/badge/net462-5C2D91?logo=.NET&labelColor=gray)](https://github.com/BoBoBaSs84/BB84.Notifications)
[![net48](https://img.shields.io/badge/net48-5C2D91?logo=.NET&labelColor=gray)](https://github.com/BoBoBaSs84/BB84.Notifications)
[![net80](https://img.shields.io/badge/net8.0-5C2D91?logo=.NET&labelColor=gray)](https://github.com/BoBoBaSs84/BB84.Notifications)
[![net90](https://img.shields.io/badge/net9.0-5C2D91?logo=.NET&labelColor=gray)](https://github.com/BoBoBaSs84/BB84.Notifications)

# BB84.Notifications

This repository contains relevant abstractions and implementations for one- and two-way binding of properties, for notifications about changed properties, for notifications about changed collections and for the Relay Command Pattern for synchronous and asynchronous operations.

## Usage

Depending on the application, there are several ways to skin a cat.

### The notifiable property class and interface

Designed with one or two way binding in mind.

```csharp
public INotifiableProperty<int> NotifiableInt { get; set; } = new NotifiableProperty<int>(default);
```

If the property is changed, the `PropertyChangingEventHandler` is triggered before the value changes and the `PropertyChangedEventHandler` is triggered after the value has changed.

- The `PropertyChangingEventHandler` contains via the `PropertyChangingEventArgs` the name of the property that is changing and when casted to `PropertyChangingEventArgs<T>` the old value of the property.
- The `PropertyChangedEventHandler` contains via the `PropertyChangedEventArgs` the name of the property that has changed and when casted to `PropertyChangedEventArgs<T>` the new value of the property.

Further implementation possibilities can be achieved with the help of the `INotifiableProperty<T>` interface.

### The reversible property class and interface

Designed with one or two way binding in mind and for switching back and forth. By default, only 10 values are saved, but a different value can be specified when initializing the property.

```csharp
int defaultValue = 42;
int valuesToStore = 20;
public IReversibleProperty<int> ReversibleInt { get; set; } = new ReversibleProperty<int>(defaultValue, valuesToStore);
```

The reversible property contains all the functions of the notifiable property and also provides the option of switching back and forth between the values already set.

**To get the previous value:**

```csharp
ReversibleInt.PreviousValue();
```

**To get the next value:**

```csharp
ReversibleInt.NextValue();
```

Further implementation possibilities can be achieved with the help of the `IReversibleProperty<T>` interface.

### The notifiable object base class and interface

Designed to handle changes at the class level and propagate them.

```csharp
public sealed class TestClass : NotifiableObject
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

Further implementation possibilities can be achieved with the help of the `INotifiableObject` interface.

### The notifiable collection base class and interface

The `NotifiableCollection` class provides methods to handle the change within a collection and propagate it to the outside.

```csharp
public sealed class MyStringCollection : NotifiableCollection, ICollection<string>
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

Further implementation possibilities can be achieved with the `INotifiableCollection` interface or the `INotifyCollectionChanged` and `INotifyCollectionChanging` interfaces.

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

To keep the costs of reflection as low as possible, the property and its properties to be notified are persisted when initializing the class that uses the attributes.

## API Documentation

The complete API documentation can be found [here](https://bobobass84.github.io/BB84.Notifications/).
