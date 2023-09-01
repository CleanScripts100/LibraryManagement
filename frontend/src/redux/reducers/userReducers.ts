import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";
import type { RootState } from "../store";
import axios from "axios";
import { User } from "../../utils/types";
import { toast } from "react-toastify";

// Define a type for the slice state
interface Users {
    user: User | null,
    users: Array<User>
}


export const getUser = createAsyncThunk('users/user', async () => {
  try {
    
    const response = await axios.get(
      "https://libmgtsyst.azurewebsites.net/profile",
      {
        headers: {
          "Authorization": `Bearer ${localStorage.getItem('token') as string}`
        }
      }
    );


    console.log(response);
    

    return response.data
  } catch (error) {
    console.log(error);
    
  }
})


export const updateUserprofile = createAsyncThunk('user/updateprofile', async (data: User) => {
  try {
    const response = await axios.patch(
      "https://libmgtsyst.azurewebsites.net/update-profile",
      data,
      {
        headers: {
         'Authorization': `Bearer ${localStorage.getItem("token") as string}`,
        },
      }
    );
    console.log(response);
    return response.data
  } catch (error) {
    console.log(error);
    
  }
})

export const createAdmin = createAsyncThunk(
  "user/createAdmin",
  async (data: User) => {
    try {
      const response = await axios.patch(
        "https://libmgtsyst.azurewebsites.net/admin",
        data,
        {
          headers: {
            Authorization: `Bearer ${localStorage.getItem("token") as string}`,
          },
        }
      );
      console.log(response);
      return response.data;
    } catch (error) {
      console.log(error);
    }
  }
);

export const getUsers = createAsyncThunk('users/getusers', async() => { 
  try {
    const response = await axios.get(
      "https://libmgtsyst.azurewebsites.net/api/v1/users",
      {
        headers: {
          Authorization: `Bearer ${localStorage.getItem("token") as string}`,
        },
      }
    );
    console.log(response);
    

    return response.data
  } catch (error) {
    console.log(error);
    
  }
})

export const deleteUser = createAsyncThunk("users/deleteusers", async (id:string) => {
  try {
    const response = await axios.delete(
      "https://libmgtsyst.azurewebsites.net/api/v1/users" + id,
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

export const updateUser = createAsyncThunk(
  "user/updateUser",
  async (data: User) => {
    try {
      const response = await axios.patch(
        "https://libmgtsyst.azurewebsites.net/api/v1/users/"+ data.id,
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
// Define the initial state using that type
const initialState: Users = {
    user:null,
    users:[]
};

export const userSlice = createSlice({
  name: "users",
  // `createSlice` will infer the state type from the `initialState` argument
  initialState,
  reducers: {
   
    },
  extraReducers(builder) {
    builder.addCase(getUser.fulfilled, (state, action) => { 
      if (action.payload) {
        state.user = action.payload
      }
    }).addCase(updateUserprofile.fulfilled, (state, action) => { 

      if (action.payload) { 


        toast.success('User profile updated successfully')
        state.user = action.payload
      }
    }).addCase(getUsers.fulfilled, (state, action) => { 
      if (action.payload) {
        state.users = action.payload
      }
    }).addCase(deleteUser.fulfilled, (state, action) => { 
    
        if (action.payload) { 
          toast.success('users deleted')
          window.location.href = ''
        }
      
    }).addCase(updateUser.fulfilled, (state, action) => { 
      if (action.payload) {
        toast.success('user updated successfully')
        window.location.href ="/admin/users"
        }
    })

  },
}, );

// export const { increment, decrement, incrementByAmount } = userSlice.actions;

// Other code such as selectors can use the imported `RootState` type

export default userSlice.reducer;
