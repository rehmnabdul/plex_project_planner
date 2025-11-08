# Todoist Clone - Test Strategy

## Table of Contents
1. [Testing Overview](#testing-overview)
2. [Testing Levels](#testing-levels)
3. [Test Types](#test-types)
4. [Test Coverage Requirements](#test-coverage-requirements)
5. [Testing Tools & Frameworks](#testing-tools--frameworks)
6. [Test Data Management](#test-data-management)
7. [Test Execution Strategy](#test-execution-strategy)
8. [Test Automation](#test-automation)
9. [Performance Testing](#performance-testing)
10. [Security Testing](#security-testing)

---

## Testing Overview

### Testing Philosophy
- **Test-Driven Development (TDD)**: Write tests first, then implement features
- **Comprehensive Coverage**: Aim for >80% code coverage
- **Automated Testing**: Maximize automation for regression testing
- **Continuous Testing**: Tests run on every commit and PR
- **Quality Gates**: No merge without passing tests

### Testing Principles
1. **Test Pyramid**: More unit tests, fewer integration tests, even fewer E2E tests
2. **Isolation**: Tests should be independent and isolated
3. **Repeatability**: Tests should produce consistent results
4. **Fast Execution**: Unit tests should run in milliseconds
5. **Clear Assertions**: Tests should clearly indicate what they're testing

---

## Testing Levels

### 1. Unit Testing
**Purpose**: Test individual components in isolation  
**Scope**: Methods, classes, services, repositories  
**Execution**: Fast (< 1 second per test)  
**Coverage Target**: >85%

**What to Test:**
- Business logic in domain services
- Application service methods
- Repository methods
- Entity validations
- DTO mappings
- Utility functions
- Extension methods

**Example Test Cases:**
- Task entity validation
- Priority level validation
- Due date calculation
- Recurrence pattern generation
- Dependency cycle detection
- Label name uniqueness

---

### 2. Integration Testing
**Purpose**: Test interaction between components  
**Scope**: Database operations, API endpoints, service integration  
**Execution**: Moderate (1-5 seconds per test)  
**Coverage Target**: >70%

**What to Test:**
- Database CRUD operations
- API endpoint functionality
- Service layer integration
- Authentication/Authorization flows
- File upload/download
- Real-time communication (SignalR)
- Background job execution

**Example Test Cases:**
- Create project via API
- Task creation with all relationships
- Project sharing workflow
- Comment creation with mentions
- Attachment upload and download
- Recurring task generation
- Auto-archiving job

---

### 3. End-to-End (E2E) Testing
**Purpose**: Test complete user workflows  
**Scope**: Full application stack (Backend + Frontend)  
**Execution**: Slow (10-30 seconds per test)  
**Coverage Target**: Critical user journeys

**What to Test:**
- Complete user workflows
- Cross-platform functionality
- Offline/online synchronization
- Real user scenarios
- UI interactions
- Error handling from user perspective

**Example Test Cases:**
- User registration and first project creation
- Complete task lifecycle (create → edit → complete → archive)
- Project sharing and collaboration
- Data export and import
- Theme switching
- Offline task creation and sync

---

### 4. API Contract Testing
**Purpose**: Ensure API contracts are maintained  
**Scope**: API request/response formats  
**Execution**: Fast to moderate  
**Coverage Target**: All public API endpoints

**What to Test:**
- Request validation
- Response format
- Status codes
- Error responses
- API versioning
- Backward compatibility

---

## Test Types

### Backend Tests

#### Unit Tests
**Framework**: xUnit  
**Location**: `test/Plex.ProjectPlanner.{Layer}.Tests/`

**Test Structure:**
```csharp
public class TaskAppServiceTests
{
    [Fact]
    public async Task CreateTask_WithValidInput_ShouldCreateTask()
    {
        // Arrange
        var input = new CreateTaskDto { ... };
        
        // Act
        var result = await _taskAppService.CreateAsync(input);
        
        // Assert
        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
    }
}
```

**Test Categories:**
- Domain Tests: `Plex.ProjectPlanner.Domain.Tests`
- Application Tests: `Plex.ProjectPlanner.Application.Tests`
- Infrastructure Tests: `Plex.ProjectPlanner.EntityFrameworkCore.Tests`

#### Integration Tests
**Framework**: xUnit + Testcontainers (for database)  
**Location**: `test/Plex.ProjectPlanner.{Layer}.Tests/Integration/`

**Test Structure:**
```csharp
public class TaskApiIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    [Fact]
    public async Task POST_Api_Tasks_ShouldCreateTask()
    {
        // Arrange
        var client = _factory.CreateClient();
        var dto = new CreateTaskDto { ... };
        
        // Act
        var response = await client.PostAsJsonAsync("/api/v1/tasks", dto);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}
```

#### Repository Tests
**Framework**: xUnit + In-memory database or Testcontainers  
**Location**: `test/Plex.ProjectPlanner.EntityFrameworkCore.Tests/`

**What to Test:**
- CRUD operations
- Complex queries
- Filtering and sorting
- Pagination
- Soft delete behavior
- Multi-tenancy isolation

---

### Frontend Tests

#### Unit Tests (Widget Tests)
**Framework**: Flutter Test  
**Location**: `test/unit/`

**Test Structure:**
```dart
void main() {
  testWidgets('TaskList displays tasks correctly', (WidgetTester tester) async {
    // Arrange
    final tasks = [Task(...), Task(...)];
    
    // Act
    await tester.pumpWidget(TaskList(tasks: tasks));
    
    // Assert
    expect(find.text('Task 1'), findsOneWidget);
    expect(find.text('Task 2'), findsOneWidget);
  });
}
```

**What to Test:**
- Widget rendering
- User interactions
- State changes
- Form validations
- Navigation

#### Integration Tests
**Framework**: Flutter Integration Test  
**Location**: `integration_test/`

**Test Structure:**
```dart
void main() {
  IntegrationTestWidgetsFlutterBinding.ensureInitialized();
  
  testWidgets('Complete task workflow', (WidgetTester tester) async {
    // Arrange
    await tester.pumpWidget(MyApp());
    
    // Act & Assert
    await tester.tap(find.text('New Task'));
    await tester.enterText(find.byType(TextField), 'Test Task');
    await tester.tap(find.text('Save'));
    expect(find.text('Test Task'), findsOneWidget);
  });
}
```

**What to Test:**
- Complete user flows
- Screen navigation
- API integration
- Offline functionality
- Theme switching

#### Golden Tests
**Framework**: Flutter Golden Test  
**Location**: `test/golden/`

**What to Test:**
- UI visual regression
- Theme rendering
- Responsive layouts

---

## Test Coverage Requirements

### Code Coverage Targets

| Layer | Unit Tests | Integration Tests | Total Coverage |
|-------|-----------|-------------------|----------------|
| Domain | >90% | N/A | >90% |
| Application | >85% | >70% | >80% |
| Infrastructure | >80% | >75% | >75% |
| API | >70% | >80% | >75% |
| Frontend | >80% | >60% | >70% |

### Coverage Tools
- **Backend**: Coverlet + ReportGenerator
- **Frontend**: Flutter Coverage

### Coverage Reports
- Generated on every build
- Published to CI/CD dashboard
- Block merge if coverage drops below threshold

---

## Testing Tools & Frameworks

### Backend Testing Stack

#### Test Framework
- **xUnit**: Primary testing framework
- **NUnit**: Alternative (if needed)

#### Mocking
- **Moq**: Mocking framework
- **NSubstitute**: Alternative mocking framework

#### Assertions
- **FluentAssertions**: Fluent assertion library
- **Shouldly**: Alternative assertion library

#### Test Data
- **Bogus**: Fake data generation
- **AutoFixture**: Test data builders

#### Database Testing
- **Testcontainers**: Docker-based database testing
- **In-Memory Database**: EF Core in-memory provider (for simple tests)

#### API Testing
- **Microsoft.AspNetCore.Mvc.Testing**: Web application factory
- **RestSharp**: HTTP client testing
- **WireMock**: Mock HTTP services

#### Performance Testing
- **NBomber**: Load testing
- **BenchmarkDotNet**: Performance benchmarking

### Frontend Testing Stack

#### Test Framework
- **Flutter Test**: Unit and widget testing
- **Integration Test**: E2E testing

#### Mocking
- **Mockito**: Mocking for Dart
- **Mocktail**: Alternative mocking

#### Golden Tests
- **Golden Toolkit**: Golden test utilities

#### Test Utilities
- **flutter_test**: Core testing utilities
- **integration_test**: E2E testing package

---

## Test Data Management

### Test Data Strategy

#### Unit Tests
- **Isolated Data**: Each test creates its own data
- **No Database**: Use mocks and in-memory data
- **Fast Execution**: No external dependencies

#### Integration Tests
- **Test Database**: Separate test database (Testcontainers)
- **Data Seeding**: Seed test data before test suite
- **Cleanup**: Clean up after each test or test suite
- **Isolation**: Each test should be independent

#### E2E Tests
- **Test Environment**: Dedicated test environment
- **Data Reset**: Reset data before test suite
- **Realistic Data**: Use realistic but anonymized data

### Test Data Builders

**Backend Example:**
```csharp
public class TaskBuilder
{
    private string _title = "Test Task";
    private Guid? _projectId = null;
    private int _priority = 3;
    
    public TaskBuilder WithTitle(string title)
    {
        _title = title;
        return this;
    }
    
    public TaskBuilder WithProject(Guid projectId)
    {
        _projectId = projectId;
        return this;
    }
    
    public Task Build()
    {
        return new Task
        {
            Id = Guid.NewGuid(),
            Title = _title,
            ProjectId = _projectId,
            Priority = _priority,
            // ... other properties
        };
    }
}
```

**Frontend Example:**
```dart
class TaskBuilder {
  String title = 'Test Task';
  String? projectId;
  int priority = 3;
  
  TaskBuilder withTitle(String title) {
    this.title = title;
    return this;
  }
  
  Task build() {
    return Task(
      id: Uuid().v4(),
      title: title,
      projectId: projectId,
      priority: priority,
      // ... other properties
    );
  }
}
```

---

## Test Execution Strategy

### Local Development
1. **Before Commit**: Run all unit tests
2. **Before Push**: Run unit + integration tests
3. **Before PR**: Run full test suite

### CI/CD Pipeline

#### Stage 1: Unit Tests
- **Trigger**: On every commit
- **Execution Time**: < 5 minutes
- **Failure Action**: Block commit/PR

#### Stage 2: Integration Tests
- **Trigger**: On PR creation/update
- **Execution Time**: < 15 minutes
- **Failure Action**: Block PR merge

#### Stage 3: E2E Tests
- **Trigger**: On PR approval
- **Execution Time**: < 30 minutes
- **Failure Action**: Block deployment

#### Stage 4: Performance Tests
- **Trigger**: Before production deployment
- **Execution Time**: < 1 hour
- **Failure Action**: Block deployment if performance degraded

### Test Execution Order
1. Fast unit tests first
2. Integration tests second
3. E2E tests last
4. Performance tests on schedule

---

## Test Automation

### Continuous Integration

#### GitHub Actions / Azure DevOps
```yaml
name: Test Suite

on:
  push:
    branches: [ main, develop ]
  pull_request:
    branches: [ main, develop ]

jobs:
  backend-tests:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v2
      - name: Setup .NET
        uses: actions/setup-dotnet@v1
      - name: Run Unit Tests
        run: dotnet test --collect:"XPlat Code Coverage"
      - name: Run Integration Tests
        run: dotnet test --filter Category=Integration
      
  frontend-tests:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v2
      - name: Setup Flutter
        uses: subosito/flutter-action@v1
      - name: Run Tests
        run: flutter test --coverage
```

### Test Reporting
- **Test Results**: Published to CI/CD dashboard
- **Coverage Reports**: HTML reports generated
- **Test Trends**: Track test results over time
- **Failure Notifications**: Notify team on test failures

---

## Performance Testing

### Load Testing
**Tool**: NBomber  
**Targets**:
- 500-1000 concurrent users
- 1000 requests per second
- Response time < 200ms (p95)

**Test Scenarios:**
- User login
- Create task
- List tasks
- Search tasks
- Upload attachment

### Stress Testing
**Purpose**: Find breaking points  
**Scenarios**:
- Maximum concurrent users
- Large data volumes
- Database connection limits

### Endurance Testing
**Purpose**: Test system stability over time  
**Duration**: 24-48 hours  
**Scenarios**:
- Continuous load
- Memory leaks
- Resource exhaustion

### Performance Benchmarks
**Target Metrics:**
- API response time: < 200ms (p95)
- Database query time: < 100ms (p95)
- File upload (100MB): < 5s
- Search response: < 500ms

---

## Security Testing

### Authentication Testing
- **Test Cases**:
  - Invalid credentials
  - Expired tokens
  - Token tampering
  - Refresh token rotation
  - Concurrent login sessions

### Authorization Testing
- **Test Cases**:
  - Unauthorized access attempts
  - Permission boundary testing
  - Multi-tenant isolation
  - Resource access control

### Input Validation Testing
- **Test Cases**:
  - SQL injection attempts
  - XSS attempts
  - File upload validation
  - Size limit enforcement
  - Type validation

### Security Scanning
- **Tools**:
  - OWASP ZAP
  - SonarQube security analysis
  - Dependency vulnerability scanning

---

## Test Maintenance

### Test Code Quality
- **Principles**:
  - DRY (Don't Repeat Yourself)
  - Clear test names
  - Single responsibility
  - Proper setup/teardown

### Test Refactoring
- **When to Refactor**:
  - Tests become slow
  - Tests are flaky
  - Test code duplication
  - Hard to understand tests

### Test Documentation
- **Documentation Requirements**:
  - Test purpose
  - Test data requirements
  - Expected behavior
  - Known limitations

---

## Test Metrics & Reporting

### Key Metrics
1. **Test Coverage**: Percentage of code covered
2. **Test Execution Time**: Total time to run all tests
3. **Test Pass Rate**: Percentage of passing tests
4. **Flaky Test Rate**: Percentage of intermittently failing tests
5. **Bug Detection Rate**: Bugs found by tests vs. production

### Reporting
- **Daily**: Test execution summary
- **Weekly**: Coverage trends
- **Monthly**: Test quality metrics
- **On Release**: Full test report

---

## Test Checklist for Each Feature

### Before Implementation
- [ ] Write unit tests for domain logic
- [ ] Write unit tests for application services
- [ ] Write integration tests for API endpoints
- [ ] Write E2E tests for user workflows

### During Implementation
- [ ] Run tests frequently
- [ ] Fix failing tests immediately
- [ ] Maintain test coverage >80%
- [ ] Update tests as code changes

### After Implementation
- [ ] All tests passing
- [ ] Coverage meets requirements
- [ ] Performance tests passing
- [ ] Security tests passing
- [ ] Code review includes test review

---

## Test Environment Setup

### Backend Test Environment
- **Database**: SQL Server (Testcontainers or dedicated test DB)
- **File Storage**: In-memory or test blob storage
- **Authentication**: Mock authentication for unit tests
- **Configuration**: Test-specific appsettings.json

### Frontend Test Environment
- **API Mocking**: Mock API responses for unit tests
- **Local Storage**: In-memory storage for tests
- **Platform Testing**: Test on multiple platforms (Android, iOS, Web, Desktop)

---

## Best Practices

### Test Naming
- **Format**: `MethodName_Scenario_ExpectedBehavior`
- **Example**: `CreateTask_WithValidInput_ShouldCreateTask`
- **Example**: `CreateTask_WithInvalidTitle_ShouldThrowException`

### Test Organization
- **Structure**: One test class per class under test
- **Grouping**: Group related tests in test classes
- **Categories**: Use test categories for filtering

### Test Data
- **Use Builders**: Use builder pattern for test data
- **Avoid Magic Values**: Use constants or variables
- **Realistic Data**: Use realistic but anonymized data

### Assertions
- **One Assertion Per Test**: Prefer multiple test methods over multiple assertions
- **Clear Messages**: Provide clear failure messages
- **Use FluentAssertions**: For readable assertions

### Test Isolation
- **No Shared State**: Each test should be independent
- **Clean Setup**: Set up test data in test method
- **Clean Teardown**: Clean up after each test

---

*This test strategy will be updated as testing practices evolve and new tools are adopted.*

