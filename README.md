# StackOverflow Clone Implementation with ASP.NET 6 MVC and Razor Pages

## Default User Accounts for Testing

### Super Admin Account
- **Email:** sadmin@gmail.com
- **Password:** 123456
- **Role:** admin

This super admin account has elevated privileges for administrative tasks.

### Normal User Account
- **Email:** abc@gmail.com
- **Password:** 123456
- **Role:** user

Use this normal user account for regular interactions with the system.

## Technologies Used:
- Asp.Net Core 6
- NHibernate.AspNetCore.Identity
- MS SQL Server
- NHibernate
- 3 Layers Architecture (Web, Application, Infrastructure)
- Log4net

## Project Structure

### 1. Web Layer (ASP.NET 6 MVC with Razor Pages)

#### Controllers
- `AccountController`: Handles Account-related actions (registration, login).
- `UserController`: Handles user-related actions (user question, update & delete question, Accept Answer).
- `TagController`: Manages actions related to tag (create, view with , delete).
- `PostController`: Manages actions related to questions (asking, Details view with Answer).
- `AnswerController`: Handles actions related to answers (answering, modifying).
- `VoteController`: Manages upvoting and downvoting.

#### Views
- `Account`: Razor pages for registration, login & Access Denied related actions.
- `User`: Razor pages for user-related actions.
- `Post`: Razor pages for question-related actions.
- `Answer`: Razor pages for answer-related actions.
- `Tag`: Razor pages for handling tags.

#### Shared
- Contains shared components and layouts.

## 2. Application Layer

### AnswerService

#### Methods
- `AddAnswer(Answer entity)`: Adds a new answer.
- `DeleteAnswer(Guid id)`: Deletes an answer.
- `GetAnswerById(Guid id)`: Retrieves an answer by ID.
- `UpdateAnswer(Answer entity)`: Updates an answer.

### PostService

#### Methods
- `AddPost(Post entity)`: Adds a new post.
- `UpdatePost(Post entity)`: Updates a post.
- `GetPostById(Guid id)`: Retrieves a post by ID.
- `GetAllPosts()`: Retrieves all posts.
- `GetUserPosts(Guid userId)`: Retrieves posts by a user.
- `GetPaginatePosts(Expression<Func<Post, bool>> filter = null!, int pageIndex = 1, int pageSize = 10)`: Paginates posts based on filters.
- `DeletePost(Guid id)`: Deletes a post.

### TagService

#### Methods
- `AddTag(Tag entity)`: Adds a new tag.
- `DeleteTag(Guid id)`: Deletes a tag.
- `GetTagById(Guid id)`: Retrieves a tag by ID.
- `GetAllTags()`: Retrieves all tags.

### UserService

#### Methods
- `GetUserById(Guid id)`: Retrieves a user by ID.

### VoteService

#### Methods
- `AddAnswerVote(AnswerVote entity)`: Adds a vote to an answer.
- `AddPostVote(PostVote entity)`: Adds a vote to a post.
- `UpdateAnswerVote(AnswerVote entity)`: Updates a vote on an answer.
- `UpdatePostVote(PostVote entity)`: Updates a vote on a post.
- `GetAnswerVoteByAnswerAndUser(Guid answerId, Guid userId)`: Retrieves a vote on an answer by a user.
- `GetPostVoteByPostAndUser(Guid postId, Guid userId)`: Retrieves a vote on a post by a user.
- `DeleteAnswerVote(Guid voteId)`: Deletes a vote on an answer.
- `DeletePostVote(Guid voteId)`: Deletes a vote on a post.

### 3. Infrastructure Layer

#### NHibernate Configuration
- The NHibernate configuration is set up using extension methods to provide a clear and organized configuration for the application.

#### NHibernate Identity Core
- NHibernate Identity Core is configured and extended using custom methods to integrate identity management seamlessly with NHibernate.

#### Repositories and Unit of Work
The Data Access Layer follows the Repository Pattern with a Unit of Work for efficient and organized data access.

#### UnitOfWork
- Acts as a unit of work to coordinate and manage transactions across multiple repositories.
- Ensures data consistency and integrity.

## Database Design

### Tag Entity

| Field         | Type      |
| ------------- | --------- |
| Id            | Guid      |
| TagName       | string    |
| TagDescription| string    |
| Posts         | List\<Post\> |

### Post Entity

| Field       | Type      |
| ----------- | --------- |
| Id          | Guid      |
| Title       | string    |
| Description | string    |
| CreatedDate | DateTime  |
| Answers     | List\<Answer\> |
| Votes       | List\<PostVote\> |
| User        | User      |
| Tag         | Tag       |

### Answer Entity

| Field          | Type      |
| -------------- | --------- |
| Id             | Guid      |
| AnswerText     | string    |
| AcceptedAnswer | bool      |
| CreatedDate    | DateTime  |
| Votes          | List\<AnswerVote\> |
| Post           | Post      |
| User           | User      |

### PostVote Entity

| Field    | Type      |
| -------- | --------- |
| Id       | Guid      |
| Post     | Post      |
| User     | User      |
| VoteType | Enum      |

### AnswerVote Entity

| Field    | Type      |
| -------- | --------- |
| Id       | Guid      |
| Answer   | Answer    |
| User     | User      |
| VoteType | Enum      |


## Logging

Utilizes Log4net for logging throughout the application. Configure Log4net to log important events and errors.

## Conclusion

This implementation follows a 3-layer architecture using ASP.NET 6 MVC with Razor Pages, NHibernate for data access, and Log4net for logging. The modular structure allows for easy maintenance and scalability, while the database design ensures efficient storage and retrieval of user, question, answer, and vote data.
