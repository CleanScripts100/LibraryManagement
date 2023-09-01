import { configureStore } from "@reduxjs/toolkit";
import bookReducers from "./reducers/bookReducers";
import userReducers from "./reducers/userReducers";
// ...

export const store = configureStore({
  reducer: {
    books: bookReducers,
    user: userReducers
  },
});

// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof store.getState>;
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch;
