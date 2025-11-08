# Todoist Clone - Development Plan

## Overview
This document outlines the step-by-step development plan for building a Todoist clone application using ASP.NET Core (ABP.IO Framework) as the backend and Flutter for cross-platform frontend (Mobile, Desktop, Web).

## Development Methodology
- **Test-Driven Development (TDD)**: Write tests first, then implement features
- **Incremental Development**: Small, measurable steps with testing after each feature
- **Approval-Based Workflow**: No implementation without explicit approval
- **Feature-by-Feature**: Complete backend → frontend → test cycle for each feature

---

## Phase 1: Foundation & Setup

### Step 1.1: Project Structure & Configuration
**Backend Tasks:**
- [ ] Set up CORS for Flutter apps
- [ ] Configure file upload settings (500MB limit)
- [ ] Set up application settings for configurable features
- [ ] Configure multi-tenancy options (single & multi-tenant support)

**Frontend Tasks:**
- [ ] Initialize Flutter project structure
- [ ] Set up project for mobile (Android/iOS), desktop (Windows/Linux/macOS), and web
- [ ] Configure offline storage (local database)
- [ ] Set up API client configuration
- [ ] Configure theme system (dark/light mode)

**Tests:**
- [ ] Write tests for CORS configuration
- [ ] Write tests for file upload limits
- [ ] Write tests for application settings

**Approval Required:** ✅ Before proceeding to Step 1.2

---

### Step 1.2: Authentication & Authorization Foundation
**Backend Tasks:**
- [ ] Implement email/password authentication endpoints
- [ ] Configure OAuth2/OpenID Connect endpoints
- [ ] Set up JWT token management
- [ ] Implement refresh token mechanism
- [ ] Create permission system foundation

**Frontend Tasks:**
- [ ] Create login screen
- [ ] Create registration screen
- [ ] Implement authentication state management
- [ ] Implement token storage (secure storage)
- [ ] Create authentication interceptor for API calls

**Tests:**
- [ ] Unit tests for authentication service
- [ ] Integration tests for authentication endpoints
- [ ] Unit tests for token management
- [ ] E2E tests for login/registration flow

**Approval Required:** ✅ Before proceeding to Step 1.3

---

## Phase 2: Core Domain Entities

### Step 2.1: User & Profile Management
**Backend Tasks:**
- [ ] Create User entity extensions (profile, preferences)
- [ ] Create UserProfile entity
- [ ] Implement user profile API endpoints
- [ ] Implement user preferences API (theme, language, etc.)
- [ ] Create user settings API

**Frontend Tasks:**
- [ ] Create user profile screen
- [ ] Create settings screen
- [ ] Implement theme switcher
- [ ] Implement user preferences management

**Tests:**
- [ ] Unit tests for user profile service
- [ ] Integration tests for user profile endpoints
- [ ] E2E tests for profile management

**Approval Required:** ✅ Before proceeding to Step 2.2

---

### Step 2.2: Projects Entity & Basic CRUD
**Backend Tasks:**
- [ ] Create Project entity (with hierarchical support)
- [ ] Create ProjectRepository
- [ ] Implement ProjectAppService (CRUD operations)
- [ ] Create ProjectDto and related DTOs
- [ ] Implement project hierarchy validation
- [ ] Create API endpoints for projects

**Frontend Tasks:**
- [ ] Create project list screen
- [ ] Create project detail screen
- [ ] Create project creation/edit forms
- [ ] Implement project hierarchy UI (tree view)
- [ ] Implement drag-and-drop for project reordering

**Tests:**
- [ ] Unit tests for Project entity
- [ ] Unit tests for ProjectAppService
- [ ] Integration tests for project CRUD endpoints
- [ ] Unit tests for project hierarchy logic
- [ ] E2E tests for project management

**Approval Required:** ✅ Before proceeding to Step 2.3

---

### Step 2.3: Tasks Entity & Basic CRUD
**Backend Tasks:**
- [ ] Create Task entity
- [ ] Create TaskRepository
- [ ] Implement TaskAppService (CRUD operations)
- [ ] Create TaskDto and related DTOs
- [ ] Implement task-project relationship
- [ ] Create API endpoints for tasks

**Frontend Tasks:**
- [ ] Create task list screen
- [ ] Create task detail screen
- [ ] Create task creation/edit forms
- [ ] Implement task display in project view

**Tests:**
- [ ] Unit tests for Task entity
- [ ] Unit tests for TaskAppService
- [ ] Integration tests for task CRUD endpoints
- [ ] E2E tests for task management

**Approval Required:** ✅ Before proceeding to Step 2.4

---

### Step 2.4: Subtasks
**Backend Tasks:**
- [ ] Extend Task entity for subtask support (self-referencing)
- [ ] Implement subtask validation (no circular dependencies)
- [ ] Update TaskAppService for subtask operations
- [ ] Create API endpoints for subtasks

**Frontend Tasks:**
- [ ] Update task detail screen to show subtasks
- [ ] Implement subtask creation/editing
- [ ] Implement subtask completion tracking
- [ ] Update UI to show subtask progress

**Tests:**
- [ ] Unit tests for subtask logic
- [ ] Unit tests for circular dependency prevention
- [ ] Integration tests for subtask endpoints
- [ ] E2E tests for subtask management

**Approval Required:** ✅ Before proceeding to Step 2.5

---

## Phase 3: Task Features

### Step 3.1: Priority Levels (P1-P5)
**Backend Tasks:**
- [ ] Create Priority enum (P1, P2, P3, P4, P5)
- [ ] Add Priority field to Task entity
- [ ] Update TaskDto to include Priority
- [ ] Implement priority-based filtering in queries
- [ ] Create API endpoints for priority management

**Frontend Tasks:**
- [ ] Add priority selector to task form
- [ ] Implement priority color coding
- [ ] Add priority filter to task list
- [ ] Implement priority-based sorting

**Tests:**
- [ ] Unit tests for priority enum
- [ ] Integration tests for priority filtering
- [ ] E2E tests for priority management

**Approval Required:** ✅ Before proceeding to Step 3.2

---

### Step 3.2: Due Dates & Times
**Backend Tasks:**
- [ ] Add DueDate and DueTime fields to Task entity
- [ ] Implement due date validation
- [ ] Create queries for overdue tasks
- [ ] Create queries for tasks due today/tomorrow
- [ ] Update TaskDto with due date/time fields

**Frontend Tasks:**
- [ ] Add date/time picker to task form
- [ ] Implement due date display in task list
- [ ] Create "Today" and "Upcoming" views
- [ ] Implement overdue task highlighting

**Tests:**
- [ ] Unit tests for due date validation
- [ ] Unit tests for overdue task queries
- [ ] Integration tests for due date endpoints
- [ ] E2E tests for due date management

**Approval Required:** ✅ Before proceeding to Step 3.3

---

### Step 3.3: Recurring Tasks
**Backend Tasks:**
- [ ] Create RecurrencePattern entity
- [ ] Implement recurrence rule engine (daily, weekly, monthly, yearly, custom)
- [ ] Create task generation service for recurring tasks
- [ ] Implement recurrence pattern validation
- [ ] Create API endpoints for recurring tasks

**Frontend Tasks:**
- [ ] Create recurrence pattern UI
- [ ] Implement recurrence pattern builder
- [ ] Show recurrence indicator in task list
- [ ] Implement recurrence editing

**Tests:**
- [ ] Unit tests for recurrence rule engine
- [ ] Unit tests for task generation
- [ ] Integration tests for recurring task endpoints
- [ ] E2E tests for recurring task creation

**Approval Required:** ✅ Before proceeding to Step 3.4

---

### Step 3.4: Task Dependencies
**Backend Tasks:**
- [ ] Create TaskDependency entity
- [ ] Implement dependency validation (no circular dependencies)
- [ ] Create dependency resolution service
- [ ] Implement dependency-based task blocking
- [ ] Create API endpoints for task dependencies

**Frontend Tasks:**
- [ ] Create dependency UI in task detail
- [ ] Implement dependency visualization
- [ ] Add dependency selector in task form
- [ ] Show blocked tasks indicator

**Tests:**
- [ ] Unit tests for dependency validation
- [ ] Unit tests for circular dependency detection
- [ ] Integration tests for dependency endpoints
- [ ] E2E tests for dependency management

**Approval Required:** ✅ Before proceeding to Step 3.5

---

### Step 3.5: Labels/Tags
**Backend Tasks:**
- [ ] Create Label entity
- [ ] Create TaskLabel junction entity
- [ ] Implement label CRUD operations
- [ ] Create label-based filtering queries
- [ ] Create API endpoints for labels

**Frontend Tasks:**
- [ ] Create label management screen
- [ ] Implement label selector in task form
- [ ] Add label filter to task list
- [ ] Implement label color coding

**Tests:**
- [ ] Unit tests for Label entity
- [ ] Unit tests for label filtering
- [ ] Integration tests for label endpoints
- [ ] E2E tests for label management

**Approval Required:** ✅ Before proceeding to Step 3.6

---

## Phase 4: Collaboration Features

### Step 4.1: Project Sharing
**Backend Tasks:**
- [ ] Create ProjectShare entity
- [ ] Implement sharing permissions (read, write, admin)
- [ ] Create project sharing service
- [ ] Implement share invitation system
- [ ] Create API endpoints for project sharing

**Frontend Tasks:**
- [ ] Create project sharing screen
- [ ] Implement share invitation UI
- [ ] Show shared projects indicator
- [ ] Implement permission management UI

**Tests:**
- [ ] Unit tests for sharing service
- [ ] Integration tests for sharing endpoints
- [ ] E2E tests for project sharing

**Approval Required:** ✅ Before proceeding to Step 4.2

---

### Step 4.2: Team Workspaces
**Backend Tasks:**
- [ ] Create Workspace entity
- [ ] Create WorkspaceMember entity
- [ ] Implement workspace management
- [ ] Create workspace role system
- [ ] Create API endpoints for workspaces

**Frontend Tasks:**
- [ ] Create workspace management screen
- [ ] Implement workspace switcher
- [ ] Create workspace member management UI
- [ ] Show workspace context in UI

**Tests:**
- [ ] Unit tests for workspace service
- [ ] Integration tests for workspace endpoints
- [ ] E2E tests for workspace management

**Approval Required:** ✅ Before proceeding to Step 4.3

---

### Step 4.3: Comments & Mentions
**Backend Tasks:**
- [ ] Create TaskComment entity
- [ ] Implement comment CRUD operations
- [ ] Create mention system (@username)
- [ ] Implement mention notifications
- [ ] Create API endpoints for comments

**Frontend Tasks:**
- [ ] Create comment section in task detail
- [ ] Implement comment input with mention support
- [ ] Show mentions indicator
- [ ] Implement comment editing/deletion

**Tests:**
- [ ] Unit tests for comment service
- [ ] Unit tests for mention parsing
- [ ] Integration tests for comment endpoints
- [ ] E2E tests for comments and mentions

**Approval Required:** ✅ Before proceeding to Step 4.4

---

### Step 4.4: Real-time Updates
**Backend Tasks:**
- [ ] Set up SignalR hub
- [ ] Implement real-time update broadcasting
- [ ] Create update event system
- [ ] Implement connection management

**Frontend Tasks:**
- [ ] Set up SignalR client
- [ ] Implement real-time update listeners
- [ ] Update UI on real-time events
- [ ] Handle connection state

**Tests:**
- [ ] Unit tests for SignalR hub
- [ ] Integration tests for real-time updates
- [ ] E2E tests for real-time synchronization

**Approval Required:** ✅ Before proceeding to Step 4.5

---

## Phase 5: Advanced Features

### Step 5.1: Filters & Views
**Backend Tasks:**
- [ ] Create Filter entity (saved filters)
- [ ] Implement filter query builder
- [ ] Create filter CRUD operations
- [ ] Create API endpoints for filters

**Frontend Tasks:**
- [ ] Create filter builder UI
- [ ] Implement saved filters
- [ ] Create filter presets (Today, Upcoming, Overdue, etc.)
- [ ] Implement filter application

**Tests:**
- [ ] Unit tests for filter query builder
- [ ] Integration tests for filter endpoints
- [ ] E2E tests for filter management

**Approval Required:** ✅ Before proceeding to Step 5.2

---

### Step 5.2: Search Functionality
**Backend Tasks:**
- [ ] Implement full-text search for tasks
- [ ] Implement search indexing
- [ ] Create search API endpoints
- [ ] Implement search result ranking

**Frontend Tasks:**
- [ ] Create search bar
- [ ] Implement search results screen
- [ ] Add search filters
- [ ] Implement search history

**Tests:**
- [ ] Unit tests for search service
- [ ] Integration tests for search endpoints
- [ ] E2E tests for search functionality

**Approval Required:** ✅ Before proceeding to Step 5.3

---

### Step 5.3: Attachments
**Backend Tasks:**
- [ ] Create TaskAttachment entity
- [ ] Implement file upload service (500MB limit)
- [ ] Implement file storage (blob storage)
- [ ] Create file download endpoints
- [ ] Implement file type validation

**Frontend Tasks:**
- [ ] Create attachment upload UI
- [ ] Implement file picker
- [ ] Show attachments in task detail
- [ ] Implement file preview/download

**Tests:**
- [ ] Unit tests for file upload service
- [ ] Unit tests for file size validation
- [ ] Integration tests for attachment endpoints
- [ ] E2E tests for attachment management

**Approval Required:** ✅ Before proceeding to Step 5.4

---

### Step 5.4: Activity/History Log
**Backend Tasks:**
- [ ] Create ActivityLog entity
- [ ] Implement activity logging service
- [ ] Create activity log queries
- [ ] Create API endpoints for activity log

**Frontend Tasks:**
- [ ] Create activity log view
- [ ] Show activity in task detail
- [ ] Implement activity filtering
- [ ] Add activity timeline

**Tests:**
- [ ] Unit tests for activity logging
- [ ] Integration tests for activity endpoints
- [ ] E2E tests for activity log

**Approval Required:** ✅ Before proceeding to Step 5.5

---

### Step 5.5: Reminders
**Backend Tasks:**
- [ ] Create TaskReminder entity
- [ ] Implement reminder scheduling service
- [ ] Create background job for reminders
- [ ] Create API endpoints for reminders

**Frontend Tasks:**
- [ ] Create reminder setup UI
- [ ] Show reminders in task detail
- [ ] Implement reminder notifications (in-app)
- [ ] Create reminder management

**Tests:**
- [ ] Unit tests for reminder service
- [ ] Integration tests for reminder endpoints
- [ ] E2E tests for reminders

**Approval Required:** ✅ Before proceeding to Step 5.6

---

## Phase 6: UI/UX Features

### Step 6.1: Themes (Dark/Light Mode)
**Backend Tasks:**
- [ ] Add theme preference to user settings
- [ ] Create API endpoint for theme preference

**Frontend Tasks:**
- [ ] Implement theme system
- [ ] Create dark/light theme definitions
- [ ] Implement theme switcher
- [ ] Persist theme preference

**Tests:**
- [ ] Unit tests for theme service
- [ ] Integration tests for theme endpoint
- [ ] E2E tests for theme switching

**Approval Required:** ✅ Before proceeding to Step 6.2

---

### Step 6.2: Keyboard Shortcuts
**Backend Tasks:**
- [ ] Create keyboard shortcut configuration API
- [ ] Implement shortcut customization

**Frontend Tasks:**
- [ ] Implement keyboard shortcut system
- [ ] Create shortcut help screen
- [ ] Add default shortcuts
- [ ] Implement shortcut customization UI

**Tests:**
- [ ] Unit tests for shortcut service
- [ ] E2E tests for keyboard shortcuts

**Approval Required:** ✅ Before proceeding to Step 6.3

---

### Step 6.3: Drag-and-Drop
**Backend Tasks:**
- [ ] Create task reordering API
- [ ] Create project reordering API
- [ ] Implement order persistence

**Frontend Tasks:**
- [ ] Implement drag-and-drop for tasks
- [ ] Implement drag-and-drop for projects
- [ ] Add visual feedback during drag
- [ ] Implement drop zones

**Tests:**
- [ ] Unit tests for reordering service
- [ ] Integration tests for reordering endpoints
- [ ] E2E tests for drag-and-drop

**Approval Required:** ✅ Before proceeding to Step 6.4

---

### Step 6.4: Bulk Operations
**Backend Tasks:**
- [ ] Create bulk update API
- [ ] Create bulk delete API
- [ ] Create bulk move API
- [ ] Implement bulk operation validation

**Frontend Tasks:**
- [ ] Implement multi-select UI
- [ ] Create bulk action menu
- [ ] Implement bulk update form
- [ ] Add bulk operation confirmation

**Tests:**
- [ ] Unit tests for bulk operations
- [ ] Integration tests for bulk endpoints
- [ ] E2E tests for bulk operations

**Approval Required:** ✅ Before proceeding to Step 6.5

---

## Phase 7: Data Management

### Step 7.1: Export Functionality
**Backend Tasks:**
- [ ] Implement JSON export service
- [ ] Implement CSV export service
- [ ] Implement iCal export service
- [ ] Implement Todoist format export
- [ ] Create export API endpoints

**Frontend Tasks:**
- [ ] Create export screen
- [ ] Implement format selection
- [ ] Implement export download
- [ ] Add export progress indicator

**Tests:**
- [ ] Unit tests for export services
- [ ] Integration tests for export endpoints
- [ ] E2E tests for export functionality

**Approval Required:** ✅ Before proceeding to Step 7.2

---

### Step 7.2: Import Functionality
**Backend Tasks:**
- [ ] Implement JSON import service
- [ ] Implement CSV import service
- [ ] Implement iCal import service
- [ ] Implement Todoist format import
- [ ] Create import API endpoints
- [ ] Implement import validation

**Frontend Tasks:**
- [ ] Create import screen
- [ ] Implement file upload for import
- [ ] Implement import preview
- [ ] Add import progress indicator

**Tests:**
- [ ] Unit tests for import services
- [ ] Integration tests for import endpoints
- [ ] E2E tests for import functionality

**Approval Required:** ✅ Before proceeding to Step 7.3

---

### Step 7.3: Archive & Data Retention
**Backend Tasks:**
- [ ] Create Archive entity
- [ ] Implement task archiving service
- [ ] Create background job for auto-archiving (1 month default)
- [ ] Implement configurable retention period
- [ ] Create archive management API

**Frontend Tasks:**
- [ ] Create archive view
- [ ] Implement archive/unarchive actions
- [ ] Show archive settings
- [ ] Add archive filter

**Tests:**
- [ ] Unit tests for archiving service
- [ ] Unit tests for auto-archiving job
- [ ] Integration tests for archive endpoints
- [ ] E2E tests for archiving

**Approval Required:** ✅ Before proceeding to Step 7.4

---

## Phase 8: Offline Support

### Step 8.1: Offline Data Synchronization
**Backend Tasks:**
- [ ] Implement sync token system
- [ ] Create sync API endpoints
- [ ] Implement conflict resolution strategy

**Frontend Tasks:**
- [ ] Set up local database (SQLite/Hive)
- [ ] Implement offline data storage
- [ ] Create sync service
- [ ] Implement conflict resolution UI
- [ ] Add sync status indicator

**Tests:**
- [ ] Unit tests for sync service
- [ ] Integration tests for sync endpoints
- [ ] E2E tests for offline sync

**Approval Required:** ✅ Before proceeding to Step 8.2

---

### Step 8.2: Offline Task Management
**Frontend Tasks:**
- [ ] Implement offline task creation
- [ ] Implement offline task updates
- [ ] Implement offline task deletion
- [ ] Create offline queue system
- [ ] Implement sync on reconnect

**Tests:**
- [ ] E2E tests for offline operations
- [ ] Unit tests for offline queue

**Approval Required:** ✅ Before proceeding to Step 8.3

---

## Phase 9: Notifications

### Step 9.1: In-App Notifications
**Backend Tasks:**
- [ ] Create Notification entity
- [ ] Implement notification service
- [ ] Create notification API endpoints
- [ ] Implement notification delivery

**Frontend Tasks:**
- [ ] Create notification center
- [ ] Implement notification display
- [ ] Add notification badges
- [ ] Implement notification actions

**Tests:**
- [ ] Unit tests for notification service
- [ ] Integration tests for notification endpoints
- [ ] E2E tests for notifications

**Approval Required:** ✅ Before proceeding to Step 9.2

---

## Phase 10: Performance & Optimization

### Step 10.1: Caching Strategy
**Backend Tasks:**
- [ ] Implement caching for frequently accessed data
- [ ] Configure cache invalidation
- [ ] Implement cache warming

**Tests:**
- [ ] Unit tests for caching
- [ ] Performance tests

**Approval Required:** ✅ Before proceeding to Step 10.2

---

### Step 10.2: Database Optimization
**Backend Tasks:**
- [ ] Add database indexes
- [ ] Optimize queries
- [ ] Implement pagination
- [ ] Add query performance monitoring

**Tests:**
- [ ] Performance tests
- [ ] Load tests

**Approval Required:** ✅ Before proceeding to Step 10.3

---

### Step 10.3: API Optimization
**Backend Tasks:**
- [ ] Implement response compression
- [ ] Add API rate limiting
- [ ] Optimize DTOs
- [ ] Implement field selection

**Tests:**
- [ ] Performance tests
- [ ] Load tests

**Approval Required:** ✅ Before proceeding to final phase

---

## Phase 11: Testing & Quality Assurance

### Step 11.1: Comprehensive Testing
**Backend Tasks:**
- [ ] Complete unit test coverage (>80%)
- [ ] Complete integration test coverage
- [ ] Add API contract tests
- [ ] Performance testing

**Frontend Tasks:**
- [ ] Complete unit test coverage
- [ ] Complete widget tests
- [ ] Complete E2E tests
- [ ] Cross-platform testing

**Approval Required:** ✅ Before proceeding to Step 11.2

---

### Step 11.2: Documentation
**Backend Tasks:**
- [ ] API documentation (Swagger/OpenAPI)
- [ ] Code documentation
- [ ] Architecture documentation

**Frontend Tasks:**
- [ ] User documentation
- [ ] Developer documentation
- [ ] Setup guides

**Approval Required:** ✅ Project Complete

---

## Development Workflow

### For Each Step:
1. **Review Step**: Review the step requirements
2. **Write Tests**: Write all tests first (TDD)
3. **Backend Implementation**: Implement backend features
4. **Backend Testing**: Run and verify backend tests
5. **Frontend Implementation**: Implement frontend features
6. **Frontend Testing**: Run and verify frontend tests
7. **Integration Testing**: Test end-to-end functionality
8. **Approval Request**: Request approval before next step
9. **Documentation**: Update documentation if needed

### Testing Checklist for Each Feature:
- [ ] Unit tests written and passing
- [ ] Integration tests written and passing
- [ ] E2E tests written and passing
- [ ] Manual testing completed
- [ ] Code review completed
- [ ] Documentation updated

---

## Notes
- All steps require explicit approval before proceeding
- Each step should be small and measurable
- Testing is mandatory at each step
- Backend → Frontend → Test cycle for each feature
- Maintain code quality and documentation throughout

---

## Estimated Timeline
- **Phase 1**: 2-3 weeks
- **Phase 2**: 3-4 weeks
- **Phase 3**: 4-5 weeks
- **Phase 4**: 3-4 weeks
- **Phase 5**: 4-5 weeks
- **Phase 6**: 2-3 weeks
- **Phase 7**: 2-3 weeks
- **Phase 8**: 2-3 weeks
- **Phase 9**: 1-2 weeks
- **Phase 10**: 2-3 weeks
- **Phase 11**: 2-3 weeks

**Total Estimated Time**: 27-38 weeks (6-9 months)

---

*This plan is subject to change based on requirements and feedback during development.*

