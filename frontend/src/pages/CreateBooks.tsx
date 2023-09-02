import { Option, Select, Stack } from "@mui/joy";
import React, { useState } from "react";
import CustomInput from "../components/CustomInput";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import { useForm } from "react-hook-form";
import { toast } from "react-toastify";
import { useAppDispatch } from "../hooks/reduxHooks";
import { createBook } from "../redux/reducers/bookReducers";
import CustomAuthorInput from "../components/CustomAuthorInput";
const schema = yup.object({
  title: yup.string().required("title is required"),

  isbn: yup.string().required("isbn is required"),
  publishedYear: yup
    .number()
    .required("published year is required")
    .typeError("published year must be a number").min(1000, 'invalid year'),
  description: yup.string().required("description is required"),
  pageCount: yup.number().required("page count is required").typeError("page count must be a number").min(3, 'number must be above 1'),
  inventoryCount: yup.number().required("inventory count is required").typeError("inventory count must be a number").min(3, 'number must be above 1'),
  genre: yup.string().required("genre is required"),
});
const CreateBooks = () => {
const [images, setimages] = useState<string[]>([]);
const [author, setauthor] = useState<string[]>([]);
    const {
      register,
      handleSubmit,
      setValue,
      reset,
      formState: { errors },
    } = useForm({
      resolver: yupResolver(schema),
      defaultValues: {
       
      },
    });
  const bookGenres = [
    {
      value:  "Novel", text: "Novel",
    },
    {
      value:  "ResearchPaper", text: "Research Paper",
    },
    {
      value:  "Fiction", text: "Fiction",
    },
 
    {
      value:  "TextBook", text: "Text Book",
    },
    {
      value:  "Other", text: "Other",
    },
 

  
  ];
  const handleChange = (
    event: React.SyntheticEvent | null,
    newValue: string | null
  ) => {
  setValue('genre', newValue as string);
  };
  const dispatch =  useAppDispatch()
  // You can access and manipulate the genres as needed
  const onsubmit = handleSubmit(async(data)=> {
    try {
    
      if (images.length < 4) {
        toast.error('please add atleast 4 images')
        return
      }
      if (author.length < 1) { 
            toast.error("please add atleast 1 book author");
            return;
      }


      dispatch(createBook({ ...data, images, author, publishedYear: data.publishedYear.toString()}));
      reset()
      setimages([])
      setauthor([])
    
  } catch (error) {
    
  }
})
  return (
    <div>
      <div className="max-w-screen-xl mx-auto px-4 pt-4 md:px-8 border-b pb-2 sticky top-0 bg-white mb-3">
        <div className="max-w-lg">
          <h3 className="text-gray-800 text-2xl font-bold">Create Book</h3>
          <p className="text-gray-600 mt-2">
            Create new Books for your readers.
          </p>
        </div>
      </div>

      <div className="flex items-center justify-center p-3">
        <div className="mx-auto w-full max-w-[60%] bg-white">
          <form onSubmit={onsubmit}>

            <div className="grid md:grid-cols-2 gap-2 mb-5 w-full">

            <div className=" ">
              <label
                htmlFor="title"
                className="mb-3 block text-base font-medium text-[#07074D]"
              >
                Title
              </label>
              <input
                type="text"
                placeholder="Book Title"
                {...register("title")}
                className="w-full rounded-md border border-[#e0e0e0] bg-white py-3 px-6 text-base font-medium text-[#6B7280] outline-none focus:border-[#6A64F1] focus:shadow-md"
              />
              {errors.title && (
                <p className="text-red-500 text-sm mt-1">
                  {errors.title.message}
                </p>
              )}
            </div>

            <div className="">
              <label
                htmlFor="pubished year"
                className="mb-3 block text-base font-medium text-[#07074D]"
              >
                Published Year
              </label>
              <input
                type="text"
                placeholder="Enter Book Published Year"
                {...register("publishedYear")}
                className="w-full rounded-md border border-[#e0e0e0] bg-white py-3 px-6 text-base font-medium text-[#6B7280] outline-none focus:border-[#6A64F1] focus:shadow-md"
              />
              {errors.publishedYear && (
                <p className="text-red-500 text-sm mt-1">
                  {errors.publishedYear.message}
                </p>
              )}
            </div>
            </div>

            <div className="grid md:grid-cols-3 gap-2 mb-5 w-full">
              
            <div className="mb-5">
              <label
                htmlFor="pubished year"
                className="mb-3 block text-base font-medium text-[#07074D]"
              >
               ISBN
              </label>
              <input
                type="text"
                placeholder="Enter Book ISBN"
                {...register("isbn")}
                className="w-full rounded-md border border-[#e0e0e0] bg-white py-3 px-6 text-base font-medium text-[#6B7280] outline-none focus:border-[#6A64F1] focus:shadow-md"
              />
              {errors.isbn && (
                <p className="text-red-500 text-sm mt-1">
                  {errors.isbn.message}
                </p>
              )}
            </div>
            <div className="mb-5">
              <label
                htmlFor="page count"
                className="mb-3 block text-base font-medium text-[#07074D]"
              >
                Page Count
              </label>
              <input
                type="text"
                  placeholder="Page Count"
                defaultValue={0}
                {...register("pageCount")}
                className="w-full rounded-md border border-[#e0e0e0] bg-white py-3 px-6 text-base font-medium text-[#6B7280] outline-none focus:border-[#6A64F1] focus:shadow-md"
              />
              {errors.pageCount && (
                <p className="text-red-500 text-sm mt-1">
                  {errors.pageCount.message}
                </p>
              )}
            </div>
            <div className="mb-5">
              <label
                htmlFor="page count"
                className="mb-3 block text-base font-medium text-[#07074D]"
              >
                Inventory Count
              </label>
              <input
                type="text"
                  placeholder="Inventory Count"
                defaultValue={0}
                {...register("inventoryCount")}
                className="w-full rounded-md border border-[#e0e0e0] bg-white py-3 px-6 text-base font-medium text-[#6B7280] outline-none focus:border-[#6A64F1] focus:shadow-md"
              />
              {errors.inventoryCount && (
                <p className="text-red-500 text-sm mt-1">
                  {errors.inventoryCount.message}
                </p>
              )}
            </div>
             </div>
            <div className="mb-5">
              <label
                htmlFor="page count"
                className="mb-3 block text-base font-medium text-[#07074D]"
              >
                Author
              </label>
            <CustomAuthorInput setvalues={setauthor} values={author}/>
           
            </div>
            <div className="mb-5">
              <label
                htmlFor="page count"
                className="mb-3 block text-base font-medium text-[#07074D]"
              >
                Images
              </label>

              <CustomInput images={images} setimages={setimages} />
            </div>

            <div className="genre mb-5">
              <label
                htmlFor="page count"
                className="mb-3 block text-base font-medium text-[#07074D]"
              >
                Genre
              </label>
              <Stack spacing={2} alignItems="flex-start">
                <Select
                  placeholder="Select a Genre"
                  name="foo"
                  required
                  onChange={handleChange}
                  sx={{ minWidth: "100%" }}
                >
                  {bookGenres.map((genre) => (
                    <Option value={genre.value}>{genre.text}</Option>
                  ))}
                </Select>
              </Stack>
            </div>

            <div>
              <textarea
                cols={20}
                rows={10}
                {...register("description")}
                className="hover:shadow-form w-full border rounded-md p-3 border-[#e0e0e0]  text-base font-semibold "
              ></textarea>

              {errors.description && (
                <p className="text-red-500 text-sm mt-1">
                  {errors.description.message}
                </p>
              )}
            </div>
            <div>
              <button className="hover:shadow-form w-full rounded-md bg-[#6A64F1] py-3 px-8 text-center text-base font-semibold text-white outline-none">
                Create
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
};

export default CreateBooks;
