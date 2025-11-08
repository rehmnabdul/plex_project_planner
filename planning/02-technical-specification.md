# Todoist Clone - Technical Specification

## Table of Contents
1. [Architecture Overview](#architecture-overview)
2. [Technology Stack](#technology-stack)
3. [Backend Architecture](#backend-architecture)
4. [Frontend Architecture](#frontend-architecture)
5. [Database Design](#database-design)
6. [API Design](#api-design)
7. [Security](#security)
8. [Performance Requirements](#performance-requirements)
9. [Scalability Considerations](#scalability-considerations)
10. [Deployment Architecture](#deployment-architecture)

---

## Architecture Overview

### System Architecture
```
┌─────────────────────────────────────────────────────────────┐
│                    Flutter Applications                      │
│  ┌──────────┐  ┌──────────┐  ┌──────────┐  ┌──────────┐   │
│  │  Mobile  │  │ Desktop  │  │   Web    │  │  Offline │   │
│  │ (iOS/    │  │ (Win/    │  │          │  │   Sync   │   │
│  │ Android) │  │Linux/Mac)│  │          │  │          │   │
│  └────┬─────┘  └────┬─────┘  └────┬─────┘  └────┬─────┘   │
└───────┼─────────────┼─────────────┼─────────────┼─────────┘
        │             │             │             │
        └─────────────┴─────────────┴─────────────┘
                          │
                          ▼
        ┌─────────────────────────────────────┐
        │      API Gateway / Load Balancer     │
        └─────────────────┬─────────────────────┘
                          │
                          ▼
        ┌─────────────────────────────────────┐
        │    ASP.NET Core API (ABP.IO)         │
        │  ┌───────────────────────────────┐  │
        │  │  Application Layer            │  │
        │  │  - AppServices                │  │
        │  │  - DTOs                       │  │
        │  └───────────────────────────────┘  │
        │  ┌───────────────────────────────┐  │
        │  │  Domain Layer                 │  │
        │  │  - Entities                   │  │
        │  │  - Domain Services            │  │
        │  └───────────────────────────────┘  │
        │  ┌───────────────────────────────┐  │
        │  │  Infrastructure Layer         │  │
        │  │  - EF Core                    │  │
        │  │  - Repositories               │  │
        │  └───────────────────────────────┘  │
        └─────────────────┬─────────────────────┘
                          │
        ┌─────────────────┴─────────────────────┐
        │                                         │
        ▼                                         ▼
┌───────────────┐                      ┌───────────────┐
│  SQL Server   │                      │  Blob Storage │
│   Database    │                      │  (Attachments)│
└───────────────┘                      └───────────────┘
```

### Key Architectural Principles
- **Layered Architecture**: Clear separation of concerns (Domain, Application, Infrastructure, Presentation)
- **Domain-Driven Design (DDD)**: Business logic in domain layer
- **CQRS Pattern**: Separate read and write operations where beneficial
- **Repository Pattern**: Data access abstraction
- **Dependency Injection**: Loose coupling between layers
- **Multi-tenancy**: Support for both single and multi-tenant scenarios

---

## Technology Stack

### Backend
- **Framework**: ASP.NET Core 9.0
- **Application Framework**: ABP.IO Framework 9.3.5
- **ORM**: Entity Framework Core
- **Database**: SQL Server
- **Authentication**: OpenIddict (OAuth2/OpenID Connect)
- **Real-time Communication**: SignalR
- **File Storage**: ABP Blob Storage (configurable to Azure Blob/AWS S3)
- **Caching**: Redis (optional, for production)
- **Background Jobs**: Hangfire or ABP Background Jobs
- **API Documentation**: Swagger/OpenAPI
- **Testing**: xUnit, Moq, FluentAssertions, Testcontainers

### Frontend
- **Framework**: Flutter 3.x
- **State Management**: Provider or Riverpod
- **Local Storage**: SQLite (via sqflite) or Hive
- **HTTP Client**: Dio
- **Real-time**: SignalR Client for Flutter
- **Offline Sync**: Custom sync service
- **UI Components**: Material Design 3 / Cupertino
- **Testing**: Flutter Test, Integration Test, Golden Tests

### Development Tools
- **IDE**: Visual Studio / Rider (Backend), VS Code / Android Studio (Frontend)
- **Version Control**: Git
- **CI/CD**: GitHub Actions / Azure DevOps
- **Code Quality**: SonarQube, ESLint (if applicable)

---

## Backend Architecture

### Layer Structure (ABP.IO)

#### 1. Domain Layer (`Plex.ProjectPlanner.Domain`)
**Responsibilities:**
- Entity definitions
- Domain services
- Domain events
- Value objects
- Business rules and validations

**Key Entities:**
- User (extended from ABP Identity)
- Project
- Task
- Subtask (self-referencing Task)
- Label
- TaskLabel (junction)
- TaskDependency
- TaskComment
- TaskAttachment
- TaskReminder
- Filter
- Workspace
- WorkspaceMember
- ProjectShare
- ActivityLog
- Notification
- RecurrencePattern

#### 2. Application Layer (`Plex.ProjectPlanner.Application`)
**Responsibilities:**
- Application services (AppServices)
- DTOs (Data Transfer Objects)
- AutoMapper profiles
- Application-specific business logic
- Permission definitions

**Key Services:**
- ProjectAppService
- TaskAppService
- LabelAppService
- CommentAppService
- AttachmentAppService
- FilterAppService
- WorkspaceAppService
- ShareAppService
- SearchAppService
- ExportAppService
- ImportAppService
- SyncAppService
- NotificationAppService

#### 3. Application Contracts Layer (`Plex.ProjectPlanner.Application.Contracts`)
**Responsibilities:**
- DTOs
- Service interfaces
- Permission definitions
- Enums and constants

#### 4. Infrastructure Layer (`Plex.ProjectPlanner.EntityFrameworkCore`)
**Responsibilities:**
- DbContext configuration
- Entity configurations
- Repository implementations
- Database migrations
- Seed data

#### 5. HTTP API Layer (`Plex.ProjectPlanner.HttpApi`)
**Responsibilities:**
- REST API controllers
- Request/response models
- API documentation

### Authentication & Authorization
- **Primary**: OAuth2/OpenID Connect (OpenIddict)
- **Secondary**: Email/Password (via ABP Identity)
- **Token Type**: JWT (JSON Web Tokens)
- **Token Lifetime**: 
  - Access Token: 1 hour
  - Refresh Token: 30 days
- **Permissions**: ABP Permission System
- **Multi-tenancy**: Tenant-based isolation

### Real-time Communication
- **Technology**: SignalR
- **Hubs**:
  - TaskHub: Task updates
  - ProjectHub: Project updates
  - NotificationHub: Real-time notifications
  - CommentHub: Comment updates

### Background Jobs
- Recurring task generation
- Auto-archiving (configurable retention)
- Reminder notifications
- Activity log cleanup
- File cleanup (orphaned attachments)

### Caching Strategy
- **User Preferences**: In-memory cache (5 min TTL)
- **Project List**: Distributed cache (15 min TTL)
- **Labels**: Distributed cache (30 min TTL)
- **Filters**: In-memory cache (10 min TTL)

---

## Frontend Architecture

### Flutter Project Structure
```
lib/
├── main.dart
├── app/
│   ├── app.dart
│   └── routes.dart
├── core/
│   ├── constants/
│   ├── theme/
│   ├── utils/
│   └── services/
│       ├── api_service.dart
│       ├── auth_service.dart
│       ├── sync_service.dart
│       └── storage_service.dart
├── data/
│   ├── models/
│   ├── repositories/
│   └── local/
│       └── database.dart
├── domain/
│   ├── entities/
│   └── repositories/
├── presentation/
│   ├── screens/
│   ├── widgets/
│   ├── providers/
│   └── blocs/ (if using BLoC)
└── test/
```

### State Management
- **Primary**: Provider or Riverpod
- **Local State**: StatefulWidget for simple UI state
- **Global State**: Provider/Riverpod for app-wide state
- **Offline State**: Local database with sync queue

### Offline Support
- **Local Database**: SQLite (via sqflite)
- **Sync Strategy**: 
  - Last sync token tracking
  - Conflict resolution (last-write-wins with manual override)
  - Sync queue for offline operations
- **Sync Triggers**:
  - App startup
  - Network reconnection
  - Manual sync
  - Periodic sync (every 5 minutes when online)

### Theme System
- **Light Theme**: Material Design 3 light theme
- **Dark Theme**: Material Design 3 dark theme
- **Theme Persistence**: Local storage
- **Theme Switching**: Runtime theme switching

### Platform-Specific Features
- **Mobile**: 
  - Push notifications (future)
  - Biometric authentication (future)
  - Haptic feedback
- **Desktop**:
  - Keyboard shortcuts
  - System tray integration
  - Native menus
- **Web**:
  - PWA support
  - Service workers
  - Browser storage

---

## Database Design

### Database Provider
- **Primary**: SQL Server
- **Development**: LocalDB or SQL Server Express
- **Production**: SQL Server Standard/Enterprise

### Naming Conventions
- **Tables**: PascalCase (e.g., `Projects`, `Tasks`)
- **Columns**: PascalCase (e.g., `Id`, `Name`, `CreatedAt`)
- **Foreign Keys**: `{Entity}Id` (e.g., `ProjectId`, `UserId`)
- **Indexes**: `IX_{Table}_{Columns}` (e.g., `IX_Tasks_ProjectId`)

### Key Database Features
- **Soft Delete**: IsDeleted flag on entities
- **Audit Fields**: CreatedAt, CreatedBy, ModifiedAt, ModifiedBy
- **Multi-tenancy**: TenantId column (when multi-tenant)
- **Concurrency**: RowVersion for optimistic concurrency

### Indexing Strategy
- Primary keys (clustered)
- Foreign keys (non-clustered)
- Frequently queried columns:
  - Tasks: ProjectId, DueDate, Priority, Status
  - Projects: UserId, ParentId
  - TaskLabels: TaskId, LabelId
  - TaskDependencies: TaskId, DependsOnTaskId

### Database Migrations
- **Tool**: EF Core Migrations
- **Strategy**: Code-based migrations
- **Naming**: `{Timestamp}_{Description}`
- **Execution**: Via DbMigrator application

---

## API Design

### RESTful API Principles
- **Resource-based URLs**: `/api/app/projects`, `/api/app/tasks`
- **HTTP Methods**: GET, POST, PUT, PATCH, DELETE
- **Status Codes**: Standard HTTP status codes
- **Response Format**: JSON
- **Error Format**: ABP standard error response

### API Endpoints Structure

#### Authentication
- `POST /api/app/auth/login` - Email/Password login
- `POST /api/app/auth/register` - User registration
- `POST /api/app/auth/refresh` - Refresh token
- `POST /api/app/auth/logout` - Logout
- `GET /api/app/auth/me` - Get current user

#### Projects
- `GET /api/app/project` - List projects
- `GET /api/app/project/{id}` - Get project
- `POST /api/app/project` - Create project
- `PUT /api/app/project/{id}` - Update project
- `DELETE /api/app/project/{id}` - Delete project
- `POST /api/app/project/{id}/reorder` - Reorder projects

#### Tasks
- `GET /api/app/task` - List tasks (with filters)
- `GET /api/app/task/{id}` - Get task
- `POST /api/app/task` - Create task
- `PUT /api/app/task/{id}` - Update task
- `PATCH /api/app/task/{id}/complete` - Complete task
- `PATCH /api/app/task/{id}/uncomplete` - Uncomplete task
- `DELETE /api/app/task/{id}` - Delete task
- `POST /api/app/task/bulk` - Bulk operations
- `POST /api/app/task/{id}/reorder` - Reorder tasks

#### Subtasks
- `GET /api/app/task/{id}/subtask` - List subtasks
- `POST /api/app/task/{id}/subtask` - Create subtask
- `PUT /api/app/task/{taskId}/subtask/{subtaskId}` - Update subtask
- `DELETE /api/app/task/{taskId}/subtask/{subtaskId}` - Delete subtask

#### Labels
- `GET /api/app/label` - List labels
- `POST /api/app/label` - Create label
- `PUT /api/app/label/{id}` - Update label
- `DELETE /api/app/label/{id}` - Delete label

#### Comments
- `GET /api/app/task/{id}/comment` - List comments
- `POST /api/app/task/{id}/comment` - Create comment
- `PUT /api/app/comment/{id}` - Update comment
- `DELETE /api/app/comment/{id}` - Delete comment

#### Attachments
- `GET /api/app/task/{id}/attachment` - List attachments
- `POST /api/app/task/{id}/attachment` - Upload attachment
- `GET /api/app/attachment/{id}/download` - Download attachment
- `DELETE /api/app/attachment/{id}` - Delete attachment

#### Search
- `GET /api/app/search?q={query}` - Search tasks and projects

#### Export/Import
- `POST /api/app/export` - Export data
- `POST /api/app/import` - Import data

#### Sync
- `GET /api/app/sync` - Get sync data
- `POST /api/app/sync` - Push sync changes

### API Response Format

#### Success Response
```json
{
  "result": {
    // Response data
  }
}
```

#### Error Response
```json
{
  "error": {
    "code": "ERROR_CODE",
    "message": "Error message",
    "details": "Detailed error information",
    "data": {}
  }
}
```

### Pagination
- **Query Parameters**: `skip`, `take`, `maxResultCount`
- **Default**: 10 items per page
- **Max**: 100 items per page
- **Response**: Includes `totalCount` and `items`

### Filtering & Sorting
- **Filtering**: Query parameters (e.g., `?projectId=123&priority=P1`)
- **Sorting**: `sortBy` and `sortOrder` parameters
- **Search**: `search` parameter for full-text search

---

## Security

### Authentication
- **JWT Tokens**: Secure token generation and validation
- **Token Storage**: Secure storage on client (encrypted)
- **Token Refresh**: Automatic refresh before expiration
- **Password Policy**: 
  - Minimum 8 characters
  - At least one uppercase, lowercase, number, special character
  - Password hashing: PBKDF2

### Authorization
- **Role-Based Access Control (RBAC)**: ABP permission system
- **Resource-Based Permissions**: Per-project/task permissions
- **Multi-tenancy Isolation**: Tenant-based data isolation

### Data Protection
- **HTTPS**: Required for all API calls
- **Input Validation**: Server-side validation for all inputs
- **SQL Injection Prevention**: Parameterized queries (EF Core)
- **XSS Prevention**: Input sanitization
- **CSRF Protection**: Token-based CSRF protection

### File Upload Security
- **File Size Limit**: 500MB per file
- **File Type Validation**: Whitelist of allowed extensions
- **Virus Scanning**: Optional (future enhancement)
- **Storage Isolation**: Tenant-based file storage

### API Security
- **Rate Limiting**: Per-user and per-IP rate limits
- **CORS**: Configured for Flutter app origins
- **API Keys**: Not used (OAuth2 only)

---

## Performance Requirements

### Response Time Targets
- **API Response Time**: < 200ms (p95)
- **Database Query Time**: < 100ms (p95)
- **File Upload**: < 5s for 100MB file
- **Search Response**: < 500ms

### Throughput Targets
- **Concurrent Users**: 500-1000 users
- **Requests per Second**: 1000 RPS
- **Database Connections**: Max 100 concurrent connections

### Optimization Strategies
- **Database Indexing**: Strategic indexes on frequently queried columns
- **Caching**: Aggressive caching for read-heavy operations
- **Pagination**: All list endpoints paginated
- **Lazy Loading**: Avoid N+1 queries
- **Connection Pooling**: Optimized connection pool settings
- **Response Compression**: Gzip compression for API responses

---

## Scalability Considerations

### Horizontal Scaling
- **Stateless API**: All API instances are stateless
- **Load Balancing**: Multiple API instances behind load balancer
- **Database**: Read replicas for read-heavy operations
- **File Storage**: Distributed blob storage (Azure Blob/AWS S3)

### Vertical Scaling
- **Database**: Can scale up SQL Server instance
- **API Server**: Can increase server resources

### Future Scalability Enhancements
- **Microservices**: Potential split into microservices if needed
- **CQRS**: Separate read/write models for high-traffic endpoints
- **Event Sourcing**: For audit and history (future consideration)
- **CDN**: For static assets and file downloads

---

## Deployment Architecture

### Development Environment
- **Backend**: Local IIS Express / Kestrel
- **Database**: LocalDB or SQL Server Express
- **Frontend**: Flutter development server

### Staging Environment
- **Backend**: Azure App Service / AWS Elastic Beanstalk
- **Database**: SQL Server (managed instance)
- **Frontend**: Web deployment + mobile app builds

### Production Environment
- **Backend**: 
  - Azure App Service (Linux) or
  - AWS ECS/Fargate or
  - Kubernetes cluster
- **Database**: 
  - Azure SQL Database or
  - AWS RDS SQL Server
- **File Storage**: 
  - Azure Blob Storage or
  - AWS S3
- **CDN**: Azure CDN / CloudFront (for file downloads)
- **Monitoring**: Application Insights / CloudWatch
- **Logging**: Centralized logging (Serilog, Application Insights)

### CI/CD Pipeline
1. **Source Control**: Git (GitHub/GitLab/Azure DevOps)
2. **Build**: Automated build on push/PR
3. **Test**: Automated test execution
4. **Deploy**: Automated deployment to staging
5. **Production**: Manual approval for production deployment

---

## Monitoring & Logging

### Application Monitoring
- **Performance Metrics**: Response times, throughput
- **Error Tracking**: Exception logging and alerting
- **Health Checks**: API health check endpoints
- **Database Monitoring**: Query performance, connection pool

### Logging
- **Log Levels**: Debug, Info, Warning, Error, Critical
- **Structured Logging**: JSON format
- **Log Aggregation**: Centralized log storage
- **Retention**: 30 days for production logs

---

## Backup & Recovery

### Database Backup
- **Frequency**: Daily full backup, hourly transaction log backup
- **Retention**: 30 days
- **Testing**: Monthly restore testing

### File Storage Backup
- **Strategy**: Geo-redundant storage (Azure/AWS)
- **Versioning**: File versioning enabled

### Disaster Recovery
- **RTO (Recovery Time Objective)**: 4 hours
- **RPO (Recovery Point Objective)**: 1 hour
- **DR Plan**: Documented and tested annually

---

*This technical specification is a living document and will be updated as the project evolves.*

