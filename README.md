# BB84.Notifications

[![CI](https://github.com/BoBoBaSs84/BB84.Notifications/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/BoBoBaSs84/BB84.Notifications/actions/workflows/ci.yml)
[![CD](https://github.com/BoBoBaSs84/BB84.Notifications/actions/workflows/cd.yml/badge.svg?branch=main)](https://github.com/BoBoBaSs84/BB84.Notifications/actions/workflows/cd.yml)
[![CodeQL](https://github.com/BoBoBaSs84/BB84.Notifications/actions/workflows/github-code-scanning/codeql/badge.svg?branch=main)](https://github.com/BoBoBaSs84/BB84.Notifications/actions/workflows/github-code-scanning/codeql)
[![Dependabot](https://github.com/BoBoBaSs84/BB84.Notifications/actions/workflows/dependabot/dependabot-updates/badge.svg?branch=main)](https://github.com/BoBoBaSs84/BB84.Notifications/actions/workflows/dependabot/dependabot-updates)

[![C#](https://img.shields.io/badge/C%23-13.0-239120)](https://github.com/BoBoBaSs84/BB84.Notifications)
[![Issues](https://img.shields.io/github/issues/BoBoBaSs84/BB84.Notifications)](https://github.com/BoBoBaSs84/BB84.Notifications/issues)
[![Commit](https://img.shields.io/github/last-commit/BoBoBaSs84/BB84.Notifications)](https://github.com/BoBoBaSs84/BB84.Notifications/commit/main)
[![License](https://img.shields.io/github/license/BoBoBaSs84/BB84.Notifications)](https://github.com/BoBoBaSs84/BB84.Notifications/blob/main/LICENSE)
[![RepoSize](https://img.shields.io/github/repo-size/BoBoBaSs84/BB84.Notifications)](https://github.com/BoBoBaSs84/BB84.Notifications)
[![Release](https://img.shields.io/github/v/release/BoBoBaSs84/BB84.Notifications)](https://github.com/BoBoBaSs84/BB84.Notifications/releases/latest)

## 🔎 Project Overview

**BB84.Notifications** is a comprehensive .NET library that provides robust abstractions and implementations for property change notifications, collection change notifications, and command patterns. The library is designed to facilitate one-way and two-way data binding scenarios, making it an excellent choice for MVVM (Model-View-ViewModel) applications, WPF, Xamarin, MAUI, and other data-binding frameworks.

This library provides:

- **Property Change Notifications**: Support for `INotifyPropertyChanged` and `INotifyPropertyChanging` with enhanced event args
- **Collection Change Notifications**: Advanced collection change tracking with before/after events
- **Reversible Properties**: Properties with undo/redo capabilities and value history
- **Validation Support**: Built-in validation with `INotifyDataErrorInfo` implementation
- **Command Patterns**: Synchronous and asynchronous command implementations
- **Attribute-Based Notifications**: Declarative property notification chaining

## ⚡ Features

### 🔔 Enhanced Property Notifications

- Generic property change event arguments with typed old/new values
- Support for both changing (before) and changed (after) events
- Automatic notification propagation via attributes
- Reflection-optimized attribute processing

### 📦 Advanced Collection Support

- Collection change notifications with item-specific information
- Before/after change events for collections
- Support for standard collection operations (Add, Remove, Clear)
- Integration with `INotifyCollectionChanged` pattern

### ⏪ Reversible Properties

- Properties with configurable value history
- Navigate forward/backward through value changes
- Undo/redo functionality built-in
- Configurable history size

### ✅ Validation Framework

- Built-in validation using Data Annotations
- `INotifyDataErrorInfo` implementation
- Property-level and object-level validation
- Error change notifications

### ⚡ Command Pattern Implementation

- Synchronous `ActionCommand` with optional `CanExecute` logic
- Asynchronous `AsyncActionCommand` with exception handling
- Generic and non-generic command variants
- Automatic `CanExecuteChanged` notifications

## 📦 Target Frameworks

[![net20](https://img.shields.io/badge/netstandard2.0-5C2D91?logo=.NET&labelColor=gray)](https://github.com/BoBoBaSs84/BB84.Notifications)
[![net21](https://img.shields.io/badge/netstandard2.1-5C2D91?logo=.NET&labelColor=gray)](https://github.com/BoBoBaSs84/BB84.Notifications)
[![net462](https://img.shields.io/badge/net462-5C2D91?logo=.NET&labelColor=gray)](https://github.com/BoBoBaSs84/BB84.Notifications)
[![net481](https://img.shields.io/badge/net481-5C2D91?logo=.NET&labelColor=gray)](https://github.com/BoBoBaSs84/BB84.Notifications)
[![net80](https://img.shields.io/badge/net8.0-5C2D91?logo=.NET&labelColor=gray)](https://github.com/BoBoBaSs84/BB84.Notifications)
[![net90](https://img.shields.io/badge/net9.0-5C2D91?logo=.NET&labelColor=gray)](https://github.com/BoBoBaSs84/BB84.Notifications)

The library supports multiple .NET framework versions:

- **.NET Standard 2.0** - Maximum compatibility
- **.NET Standard 2.1** - Enhanced performance features
- **.NET Framework 4.6.2** - Legacy Windows applications
- **.NET Framework 4.8.1** - Latest .NET Framework
- **.NET 8.0** - Modern .NET with latest features
- **.NET 9.0** - Cutting-edge .NET version

## 💾 Installation

[![NuGet](https://img.shields.io/nuget/v/BB84.Notifications.svg?logo=nuget&logoColor=white)](https://www.nuget.org/packages/BB84.Notifications)

### Package Manager Console

```powershell
Install-Package BB84.Notifications
```

### .NET CLI

```bash
dotnet add package BB84.Notifications
```

### PackageReference (csproj)

```xml
<PackageReference Include="BB84.Notifications" Version="[latest_version]" />
```

## 🥦 Core Components

### 1. Notifiable Properties

#### INotifiableProperty&lt;T&gt;

The foundation interface for properties that support change notifications.

```csharp
public interface INotifiableProperty<T> : INotifyPropertyChanged, INotifyPropertyChanging
{
    T Value { get; set; }
    bool IsDefault { get; }
    bool IsNull { get; }
}
```

#### NotifiableProperty&lt;T&gt;

Concrete implementation of a notifiable property with implicit conversion support.

**Key Features:**

- Automatic change detection using `EqualityComparer<T>`
- Generic event arguments with typed values
- Implicit conversion operators
- Thread-safe property updates

### 2. Reversible Properties

#### IReversibleProperty&lt;T&gt;

Extends notifiable properties with history and navigation capabilities.

```csharp
public interface IReversibleProperty<T> : INotifiableProperty<T>
{
    bool CanNavigateNext { get; }
    bool CanNavigatePrevious { get; }
    void NextValue();
    void PreviousValue();
}
```

#### ReversibleProperty&lt;T&gt;

Implementation with configurable history size and value navigation.

**Key Features:**

- Configurable history size (default: 10 values)
- Forward/backward navigation through value history
- Automatic history management
- Integration with notifiable property features

### 3. Notifiable Objects

#### INotifiableObject

Base interface for objects that support property change notifications.

#### NotifiableObject

Abstract base class providing property change infrastructure.

**Key Features:**

- `SetProperty` method for automatic change detection
- Attribute-based notification propagation
- Reflection optimization with caching
- Support for computed properties

### 4. Collection Notifications

#### INotifiableCollection

Comprehensive interface for collection change notifications.

```csharp
public interface INotifiableCollection :
    INotifyCollectionChanged,
    INotifyCollectionChanging,
    INotifyPropertyChanged,
    INotifyPropertyChanging
{
}
```

#### NotifiableCollection

Abstract base class for collections with change notifications.

**Key Features:**

- Before/after collection change events
- Item-specific change information
- Integration with standard collection interfaces
- Support for batch operations

### 5. Validation Support

#### IValidatableObject

Interface for objects that support validation and error notifications.

#### ValidatableObject

Base class with built-in validation using Data Annotations.

**Key Features:**

- Integration with `System.ComponentModel.DataAnnotations`
- Property-level and object-level validation
- `INotifyDataErrorInfo` implementation
- Automatic error change notifications

### 6. Command Patterns

#### IActionCommand / IActionCommand&lt;T&gt;

Interfaces for synchronous command execution.

#### IAsyncActionCommand / IAsyncActionCommand&lt;T&gt;

Interfaces for asynchronous command execution.

**Implementations:**

- `ActionCommand` / `ActionCommand<T>` - Synchronous commands
- `AsyncActionCommand` / `AsyncActionCommand<T>` - Asynchronous commands

## 🧰 Usage Guide

### Basic Property Notification

```csharp
public class PersonViewModel : NotifiableObject
{
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;

    public string FirstName
    {
        get => _firstName;
        set => SetProperty(ref _firstName, value);
    }

    public string LastName
    {
        get => _lastName;
        set => SetProperty(ref _lastName, value);
    }
}
```

### Notifiable Properties

```csharp
public class ProductViewModel
{
    public INotifiableProperty<string> Name { get; set; } = new NotifiableProperty<string>("Default Name");
    public INotifiableProperty<decimal> Price { get; set; } = new NotifiableProperty<decimal>(0m);
    public INotifiableProperty<int> Quantity { get; set; } = new NotifiableProperty<int>(1);

    public ProductViewModel()
    {
        // Subscribe to property changes
        Name.PropertyChanged += (s, e) => Console.WriteLine($"Name changed to: {Name.Value}");
        Price.PropertyChanged += (s, e) => Console.WriteLine($"Price changed to: {Price.Value:C}");
    }
}
```

### Reversible Properties with History

```csharp
public class EditableDocument
{
    private const int HistorySize = 20;

    public IReversibleProperty<string> Content { get; set; } =
        new ReversibleProperty<string>("Initial Content", HistorySize);

    public void Undo()
    {
        if (Content.CanNavigatePrevious)
            Content.PreviousValue();
    }

    public void Redo()
    {
        if (Content.CanNavigateNext)
            Content.NextValue();
    }
}
```

### Collection Notifications

```csharp
public class ObservableStringCollection : NotifiableCollection, ICollection<string>
{
    private readonly Collection<string> _items = new();

    public int Count => _items.Count;
    public bool IsReadOnly => false;

    public void Add(string item)
    {
        // Notify before change
        RaiseCollectionChanging(CollectionChangeAction.Add, item);

        // Perform the change
        _items.Add(item);

        // Notify after change
        RaiseCollectionChanged(CollectionChangeAction.Add, item);
    }

    public bool Remove(string item)
    {
        if (!_items.Contains(item))
            return false;

        RaiseCollectionChanging(CollectionChangeAction.Remove, item);
        bool removed = _items.Remove(item);
        RaiseCollectionChanged(CollectionChangeAction.Remove, item);

        return removed;
    }

    public void Clear()
    {
        RaiseCollectionChanging(CollectionChangeAction.Refresh);
        _items.Clear();
        RaiseCollectionChanged(CollectionChangeAction.Refresh);
    }

    // ... implement other ICollection<string> members
}
```

### Validation with Data Annotations

```csharp
public class UserRegistrationViewModel : ValidatableObject
{
    private string _email = string.Empty;
    private string _password = string.Empty;
    private int _age;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email
    {
        get => _email;
        set => SetValidatedProperty(ref _email, value);
    }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    public string Password
    {
        get => _password;
        set => SetValidatedProperty(ref _password, value);
    }

    [Range(18, 120, ErrorMessage = "Age must be between 18 and 120")]
    public int Age
    {
        get => _age;
        set => SetValidatedProperty(ref _age, value);
    }

    public bool CanSubmit => IsValid && !HasErrors;
}
```

### Attribute-Based Property Notifications

```csharp
public class ShoppingCartItemViewModel : NotifiableObject
{
    private int _quantity;
    private decimal _unitPrice;

    [NotifyChanged(nameof(TotalPrice))]
    [NotifyChanging(nameof(TotalPrice))]
    public int Quantity
    {
        get => _quantity;
        set => SetProperty(ref _quantity, value);
    }

    [NotifyChanged(nameof(TotalPrice))]
    [NotifyChanging(nameof(TotalPrice))]
    public decimal UnitPrice
    {
        get => _unitPrice;
        set => SetProperty(ref _unitPrice, value);
    }

    // This property will automatically receive notifications when Quantity or UnitPrice changes
    public decimal TotalPrice => Quantity * UnitPrice;
}
```

### Command Implementation

```csharp
public class MainViewModel : NotifiableObject
{
    private string _searchText = string.Empty;
    private bool _isSearching;

    public string SearchText
    {
        get => _searchText;
        set => SetProperty(ref _searchText, value);
    }

    public bool IsSearching
    {
        get => _isSearching;
        private set => SetProperty(ref _isSearching, value);
    }

    // Synchronous command
    public IActionCommand ClearCommand { get; }

    // Asynchronous command with parameter
    public IAsyncActionCommand<string> SearchCommand { get; }

    public MainViewModel()
    {
        ClearCommand = new ActionCommand(
            execute: () => SearchText = string.Empty,
            canExecute: () => !string.IsNullOrEmpty(SearchText)
        );

        SearchCommand = new AsyncActionCommand<string>(
            execute: async (query) => await PerformSearchAsync(query),
            canExecute: (query) => !IsSearching && !string.IsNullOrWhiteSpace(query),
            onException: (ex) => Console.WriteLine($"Search failed: {ex.Message}")
        );

        // Update command states when properties change
        PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(SearchText))
            {
                ClearCommand.RaiseCanExecuteChanged();
                SearchCommand.RaiseCanExecuteChanged();
            }
        };
    }

    private async Task PerformSearchAsync(string query)
    {
        IsSearching = true;
        try
        {
            // Simulate async search
            await Task.Delay(2000);
            // Perform actual search logic here
        }
        finally
        {
            IsSearching = false;
        }
    }
}
```

## 🎛️ Advanced Scenarios

### Custom Event Arguments

The library provides strongly-typed event arguments that carry additional information:

```csharp
public void HandlePropertyChanging(object sender, PropertyChangingEventArgs e)
{
    if (e is PropertyChangingEventArgs<string> stringArgs)
    {
        string oldValue = stringArgs.OldValue;
        string propertyName = stringArgs.PropertyName;
        Console.WriteLine($"Property '{propertyName}' changing from '{oldValue}'");
    }
}

public void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
{
    if (e is PropertyChangedEventArgs<string> stringArgs)
    {
        string newValue = stringArgs.NewValue;
        string propertyName = stringArgs.PropertyName;
        Console.WriteLine($"Property '{propertyName}' changed to '{newValue}'");
    }
}
```

### Collection Change Handling

```csharp
public void HandleCollectionChanges()
{
    var collection = new ObservableStringCollection();

    collection.CollectionChanging += (sender, e) =>
    {
        Console.WriteLine($"Collection about to {e.Action}");
        if (e is CollectionChangingEventArgs<string> typedArgs)
        {
            Console.WriteLine($"Item: {typedArgs.Item}");
        }
    };

    collection.CollectionChanged += (sender, e) =>
    {
        Console.WriteLine($"Collection {e.Action} completed");
        if (e is CollectionChangedEventArgs<string> typedArgs)
        {
            Console.WriteLine($"Item: {typedArgs.Item}");
        }
    };
}
```

### Performance Considerations

#### Attribute Caching

The library optimizes reflection-based attribute processing by caching property relationships:

```csharp
// Attributes are processed once during object construction
// Subsequent property changes use cached information for optimal performance
public class OptimizedViewModel : NotifiableObject
{
    // Attribute metadata is cached automatically
    [NotifyChanged(nameof(DependentProperty1), nameof(DependentProperty2))]
    public string SourceProperty { get; set; }

    public string DependentProperty1 => $"Dependent on: {SourceProperty}";
    public string DependentProperty2 => $"Also dependent on: {SourceProperty}";
}
```

#### Memory Management

The library is designed to minimize memory allocations:

- Event argument objects are reused where possible
- Collection change notifications use efficient data structures
- Property change detection uses optimized equality comparisons

## 📔 API Reference

### Core Namespaces

- **`BB84.Notifications`** - Main classes and implementations
- **`BB84.Notifications.Interfaces`** - Core interfaces
- **`BB84.Notifications.Components`** - Event argument classes and delegates
- **`BB84.Notifications.Commands`** - Command pattern implementations
- **`BB84.Notifications.Attributes`** - Notification attributes
- **`BB84.Notifications.Extensions`** - Utility extensions

### Key Classes

| Class                   | Description                                | Key Features                             |
| ----------------------- | ------------------------------------------ | ---------------------------------------- |
| `NotifiableProperty<T>` | Generic property with change notifications | Implicit operators, typed events         |
| `ReversibleProperty<T>` | Property with value history                | Undo/redo, configurable history          |
| `NotifiableObject`      | Base class for notifiable objects          | SetProperty, attribute support           |
| `NotifiableCollection`  | Base class for notifiable collections      | Before/after events, typed notifications |
| `ValidatableObject`     | Base class with validation                 | Data annotations, error notifications    |
| `ActionCommand`         | Synchronous command implementation         | CanExecute logic, parameter support      |
| `AsyncActionCommand`    | Asynchronous command implementation        | Exception handling, cancellation         |

### Event Types

| Event Handler                    | Description                | Event Args                       |
| -------------------------------- | -------------------------- | -------------------------------- |
| `PropertyChangingEventHandler`   | Property about to change   | `PropertyChangingEventArgs<T>`   |
| `PropertyChangedEventHandler`    | Property has changed       | `PropertyChangedEventArgs<T>`    |
| `CollectionChangingEventHandler` | Collection about to change | `CollectionChangingEventArgs<T>` |
| `CollectionChangedEventHandler`  | Collection has changed     | `CollectionChangedEventArgs<T>`  |

## 🧰 Examples

### Complete MVVM Example

```csharp
// Model
public class Person
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
}

// ViewModel
public class PersonViewModel : ValidatableObject
{
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;
    private DateTime _birthDate = DateTime.Today;

    [Required(ErrorMessage = "First name is required")]
    [NotifyChanged(nameof(FullName))]
    public string FirstName
    {
        get => _firstName;
        set => SetValidatedProperty(ref _firstName, value);
    }

    [Required(ErrorMessage = "Last name is required")]
    [NotifyChanged(nameof(FullName))]
    public string LastName
    {
        get => _lastName;
        set => SetValidatedProperty(ref _lastName, value);
    }

    public DateTime BirthDate
    {
        get => _birthDate;
        set => SetProperty(ref _birthDate, value);
    }

    public string FullName => $"{FirstName} {LastName}".Trim();

    public IActionCommand SaveCommand { get; }
    public IActionCommand ResetCommand { get; }

    public PersonViewModel()
    {
        SaveCommand = new ActionCommand(
            execute: Save,
            canExecute: () => IsValid && !HasErrors
        );

        ResetCommand = new ActionCommand(Reset);

        // Update command states when validation changes
        ErrorsChanged += (s, e) => SaveCommand.RaiseCanExecuteChanged();
    }

    private void Save()
    {
        if (!IsValid) return;

        var person = new Person
        {
            FirstName = FirstName,
            LastName = LastName,
            BirthDate = BirthDate
        };

        // Save logic here
        Console.WriteLine($"Saving: {person.FirstName} {person.LastName}");
    }

    private void Reset()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        BirthDate = DateTime.Today;
    }
}
```

### Reactive Programming Pattern

```csharp
public class ReactiveViewModel : NotifiableObject
{
    private readonly INotifiableProperty<string> _searchTerm = new NotifiableProperty<string>(string.Empty);
    private readonly INotifiableProperty<List<string>> _results = new NotifiableProperty<List<string>>(new List<string>());
    private readonly INotifiableProperty<bool> _isLoading = new NotifiableProperty<bool>(false);

    public INotifiableProperty<string> SearchTerm => _searchTerm;
    public INotifiableProperty<List<string>> Results => _results;
    public INotifiableProperty<bool> IsLoading => _isLoading;

    public ReactiveViewModel()
    {
        // React to search term changes
        _searchTerm.PropertyChanged += async (s, e) =>
        {
            if (e is PropertyChangedEventArgs<string> args && !string.IsNullOrEmpty(args.NewValue))
            {
                await PerformSearchAsync(args.NewValue);
            }
        };
    }

    private async Task PerformSearchAsync(string term)
    {
        _isLoading.Value = true;
        try
        {
            // Simulate async search
            await Task.Delay(500);
            var results = Enumerable.Range(1, 10)
                .Select(i => $"{term} Result {i}")
                .ToList();

            _results.Value = results;
        }
        finally
        {
            _isLoading.Value = false;
        }
    }
}
```

## 💎 Testing

The library includes comprehensive unit tests covering all major functionality. The test suite uses MSTest framework and covers:

- Property change notifications
- Collection change events
- Command execution and cancellation
- Validation scenarios
- Attribute-based notifications
- Error handling and edge cases

### Running Tests

```bash
dotnet test
```

### Test Coverage Areas

- **Property Notifications**: All notification scenarios and event argument types
- **Collection Operations**: Add, remove, clear, and batch operations
- **Validation**: Data annotation validation and error propagation
- **Commands**: Synchronous and asynchronous execution with various parameters
- **Attributes**: Notification chaining and dependency tracking
- **Edge Cases**: Null values, default values, threading scenarios

## 🤝 Contributing

Contributions are welcome! Please follow these guidelines:

1. **Fork the repository** and create a feature branch
2. **Write tests** for new functionality
3. **Follow coding standards** and maintain consistent style
4. **Update documentation** for public API changes
5. **Submit a pull request** with a clear description

### Development Setup

1. Clone the repository
2. Open the solution in Visual Studio 2022 or VS Code
3. Restore NuGet packages
4. Build and run tests

### Code Standards

- Use C# 13.0 language features where appropriate
- Follow Microsoft naming conventions
- Include XML documentation for public APIs
- Maintain high test coverage (>90%)

## ⚖️ License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.

## 📚 Support and Resources

- **API Documentation**: [https://bobobass84.github.io/BB84.Notifications/](https://bobobass84.github.io/BB84.Notifications/)
- **NuGet Package**: [https://www.nuget.org/packages/BB84.Notifications](https://www.nuget.org/packages/BB84.Notifications)
- **GitHub Repository**: [https://github.com/BoBoBaSs84/BB84.Notifications](https://github.com/BoBoBaSs84/BB84.Notifications)
- **Issues & Feature Requests**: [GitHub Issues](https://github.com/BoBoBaSs84/BB84.Notifications/issues)
