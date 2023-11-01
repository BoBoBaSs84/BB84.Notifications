[![CI](https://github.com/BoBoBaSs84/BB84.Notifications/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/BoBoBaSs84/BB84.Notifications/actions/workflows/ci.yml)
[![Docs](https://github.com/BoBoBaSs84/BB84.Notifications/actions/workflows/docs.yml/badge.svg?branch=main)](https://github.com/BoBoBaSs84/BB84.Notifications/actions/workflows/docs.yml)
[![CodeQL](https://github.com/BoBoBaSs84/BB84.Notifications/actions/workflows/codeql.yml/badge.svg?branch=main)](https://github.com/BoBoBaSs84/BB84.Notifications/actions/workflows/codeql.yml)
[![Issues](https://img.shields.io/github/issues/BoBoBaSs84/BB84.Notifications)](https://github.com/BoBoBaSs84/BB84.Notifications/issues)
[![Commit](https://img.shields.io/github/last-commit/BoBoBaSs84/BB84.Notifications)](https://github.com/BoBoBaSs84/BB84.Notifications/commit/main)
[![Forks](https://img.shields.io/github/forks/BoBoBaSs84/BB84.Notifications)](https://github.com/BoBoBaSs84/BB84.Notifications/network)
[![Size](https://img.shields.io/github/repo-size/BoBoBaSs84/BB84.Notifications)](https://github.com/BoBoBaSs84/BB84.Notifications)
[![stars](https://img.shields.io/github/stars/BoBoBaSs84/BB84.Notifications)](https://github.com/BoBoBaSs84/BB84.Notifications/stargazers)
[![License](https://img.shields.io/github/license/BoBoBaSs84/BB84.Notifications)](https://github.com/BoBoBaSs84/BB84.Notifications/blob/main/LICENSE)

# BB84.Notifications

Contains relevant things for property one-way binding / two-way binding, for property change / changing notification and for collection change / changing notification.

# Usage

Depending on the application, there are several ways to skin a cat.

## The bindable property class and interfaces

Designed with one or two way binding in mind.

```csharp
public IBindableProperty<int> BindableInt { get; set; } = new BindableProperty<int>(default);
```

If the property is changed, the `BindablePropertyChangingEventHandler<T>` is triggered before the value changes and the `BindablePropertyChangedEventHandler<T>` is triggered after the value has changed.

- The `BindablePropertyChangingEventHandler<T>` contains via the `BindablePropertyChangingEventArgs` the old value.
- The `BindablePropertyChangedEventHandler<T>` contains via the `BindablePropertyChangedEventArgs` the new value.

Further implementation possibilities can be achieved with the help of the `IBindableProperty` interface.

## The notification property base class and interfaces

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

- The `PropertyChangingEventHandler` contains via the `CollectionChangingEventArgs` the name of the property that is changing and the old value.
- The `PropertyChangedEventHandler` contains via the `PropertyChangedEventArgs` the name of the property that has changed and the new value.

Further implementation possibilities can be achieved using the `INotifyPropertyChanged` and `INotifyPropertyChanging` interfaces.

## The notification collection base class and interfaces

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

- The `CollectionChangingEventHandler` contains via the `CollectionChangingEventArgs` the `CollectionChangeAction` and can contain the `object` before the change depending on the action.
- The `CollectionChangedEventHandler` contains via the `CollectionChangedEventArgs` the `CollectionChangeAction` and can contain the `object` after the change depending on the action.

Further implementation possibilities can be achieved with the interfaces `INotifyCollectionChanged` and `INotifyCollectionChanging`.

# Documentation

The API documentation can be found [here](https://bobobass84.github.io/BB84.Notifications/).
