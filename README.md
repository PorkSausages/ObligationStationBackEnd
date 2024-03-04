# Obligation Station Back End

## Notes
- The front end React project can be found [here](https://github.com/PorkSausages/obligation-station-ui/tree/main).
- Because of the complexity of making a React UI (at least for someone with little experience with React 🥴), I tried to avoid over-engineering the application in order to make sure that all the features outlined in the assignment were present and working in a reasonable time frame. If this were a production-ready application with multiple users, I would have taken the following approaches for better maintainability and easier future development:
  - Instead of holding all the state directly inside the `StateService`, I would have stored it inside a dedicated database (probably something like SQLite) so that it can be persisted, and abstracted the data access implementation details away into a Repository layer which would be accessed via the Service layer.
  - I would have implemented a more robust login/identity system with passwords, and some basic security (enabling HTTPS, storing hashed passwords, requiring a valid email address to be confirmed on account creation, etc).
  - I would have implemented some kind of API authentication so that the front end and any future clients would be required to authenticate with a bearer token.
  - With the additional endpoints required for an improved identity and authentication system, I would have split `User`, `Todo` and `Authentication` actions into their own dedicated controllers.
