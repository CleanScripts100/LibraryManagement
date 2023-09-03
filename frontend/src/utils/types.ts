export interface Book {
  id?: string;
  author: Array<string>;
  images: Array<string>;
  title: string;
  isbn: string;
  publishedYear: string;
  description: string;
  pageCount: number;
  inventoryCount: number;
  genre: string;
}

export interface User {
  id?: string;
  firstName: string;
  lastName: string;
  email: string;
  gender: string;
  avatar: string;
  password?: string;
  role: string;
}
