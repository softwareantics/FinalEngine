## ViewModel:
- [ ] **Separation of Concerns:**
   - [ ] Are the ViewModel classes separate from the View and Model classes?
   - [ ] Are the responsibilities of the ViewModel clear and focused?
   - [ ] Have you attempted to minimize usage of the weak reference messenger?
   - [ ] Have you attempted to minimize the amount of code-beind in the views?

- [ ] **Data Binding:**
   - [ ] Are proper INotifyPropertyChanged implementations used for properties that need to notify the UI of changes?
   - [ ] Are ICommand implementations used for handling UI interactions?

- [ ] **Commands:**
   - [ ] Are commands defined for UI actions and interactions?
   - [ ] Are command implementations lightweight and focused on their intended purpose?

- [ ] **Data Validation:**
   - [ ] Is data validation logic implemented in the ViewModel?
   - [ ] Are you using `ObservableValidator` to handle displaying validation errors?

- [ ] **Dependency Injection:**
   - [ ] Are dependencies injected into the ViewModel rather than being directly instantiated within it?

## View:
- [ ] **XAML Structure:**
   - [ ] Is the XAML well-structured with appropriate grouping of controls?
   - [ ] Are resources like styles, templates, and converters properly utilized?
   - [ ] Is the `DataContext` design instance set (ie; `d:DataContext="{x:Type vm:MyViewModel}"`

- [ ] **Data Binding:**
   - [ ] Are ViewModel properties correctly bound to the corresponding UI elements?
   - [ ] Are converters used where necessary to transform data for display?

- [ ] **UI Elements:**
   - [ ] Are UI controls appropriately chosen for their intended purposes?
   - [ ] Are UI controls styled consistently using defined styles?

- [ ] **Layout:**
   - [ ] Is the layout designed to handle different screen sizes and orientations?
   - [ ] Are appropriate layout panels (Grid, StackPanel, etc.) used to achieve desired visual structure?

## Model:
- [ ] **Data Representation:**
   - [ ] Does the Model accurately represent the data and business logic?
   - [ ] Are validation and integrity checks implemented in the Model?

- [ ] **Domain Logic:**
   - [ ] Is complex business logic kept separate from the ViewModel and View layers?
   - [ ] Are methods and properties in the Model well-named and organized?
