import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";

import axios from "axios";
import { Book } from "../../utils/types";
import { toast } from "react-toastify";

// Define a type for the slice state
interface InitialState {
  book: Book | null;
  books: Book[]
}

export const getBooks = createAsyncThunk("books/all", async () => {
try {
      const response = await axios.get(
        "https://libmgtsyst.azurewebsites.net/api/v1/books"
      );
    
    return response.data
} catch (error) {
    console.log(error);
    
}
    
});


export const getBook = createAsyncThunk("books/book", async (id:string) => {
  try {
    const response = await axios.get(
      "https://libmgtsyst.azurewebsites.net/api/v1/books/"+id
    );

    return response.data;
  } catch (error) {
    console.log(error);
  }
});


export const createBook = createAsyncThunk("books/create", async (data: Book) => {
  try {
    const response = await axios.post(
      "https://libmgtsyst.azurewebsites.net/api/v1/books/",

      data,
      {
        headers: {
          Authorization: `Bearer ${localStorage.getItem("token") as string}`,
        },
      }
    );

    return response.data;
  } catch (error) {
    console.log(error);
  }
});

export const editBook = createAsyncThunk(
  "books/edit",
  async (data: Book,) => {
    try {
      const response = await axios.patch(
        "https://libmgtsyst.azurewebsites.net/api/v1/books/"+data.id,

        data,
        {
          headers: {
            Authorization: `Bearer ${localStorage.getItem("token") as string}`,
          },
        }
      );

      return response.data;
    } catch (error) {
      console.log(error);
    }
  }
);

export const deleteBook = createAsyncThunk("books/delete", async (id:string) => {
  try {
    const response = await axios.delete(
      "https://libmgtsyst.azurewebsites.net/api/v1/books/" + id,

    
      {
        headers: {
          Authorization: `Bearer ${localStorage.getItem("token") as string}`,
        },
      }
    );

    return response.data;
  } catch (error) {
    console.log(error);
  }
});



// Define the initial state using that type
const initialState: InitialState = {
  book: null,
  books: [],
};

export const bookSlice = createSlice({
  name: "users",
  // `createSlice` will infer the state type from the `initialState` argument
  initialState,
  reducers: {},
  extraReducers(builder) {
      builder.addCase(getBooks.fulfilled, (state,actions) => {
          if (actions.payload) {
              state.books = actions.payload
          }
      }).addCase(getBook.fulfilled, (state, actions) => {
        if (actions.payload) {
          state.book = actions.payload
        }
      }).addCase(createBook.fulfilled, (state, actions) => { 
        if (actions.payload) {
          state.books.push(actions.payload)
          
          toast.success('Book created successfully')
        }
      }).addCase(deleteBook.fulfilled, (state, actions) => { 
        if (actions.payload) {
          
          toast.success('Book deleted successfully')
          setTimeout(()=> window.location.href = '')
        }
      }).addCase(editBook.fulfilled, (state, actions) => { 
        
        if (actions.payload) {
          toast.success('Book updated successfully')
          setTimeout(() => (window.location.href = ""));

        }

      })
  },
});

// export const { increment, decrement, incrementByAmount } = userSlice.actions;

// Other code such as selectors can use the imported `RootState` type

export default bookSlice.reducer;
