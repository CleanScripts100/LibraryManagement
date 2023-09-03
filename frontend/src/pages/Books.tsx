import ReactPaginate from "react-paginate";
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { useForm } from "react-hook-form";
import * as yup from "yup";
import { toast } from "react-toastify";
import {
  Button,
  Modal,
  ModalClose,
  ModalDialog,
  ModalDialogProps,
  Option,
  Select,
  Stack,
} from "@mui/joy";

import { useAppDispatch, useAppSelector } from "../hooks/reduxHooks";
import { deleteBook, editBook, getBook } from "../redux/reducers/bookReducers";
import CustomAuthorInput from "../components/CustomAuthorInput";
import CustomInput from "../components/CustomInput";
import { yupResolver } from "@hookform/resolvers/yup";
import Loader from "../components/Loader";

const schema = yup.object({
  title: yup.string().required("title is required"),
  id: yup.string().required("id is required"),
  isbn: yup.string().required("isbn is required"),
  publishedYear: yup
    .number()
    .required("published year is required")
    .typeError("published year must be a number")
    .min(1000, "invalid year"),
  description: yup.string().required("description is required"),
  pageCount: yup
    .number()
    .required("page count is required")
    .typeError("page count must be a number")
    .min(3, "number must be above 1"),
  inventoryCount: yup
    .number()
    .required("inventory count is required")
    .typeError("inventory count must be a number")
    .min(3, "number must be above 1"),
  genre: yup.string().required("genre is required"),
});

const Books = () => {
  const itemsPerPage = 5; // Number of items to display per page
  const [currentPage, setCurrentPage] = useState(0);
  const { books, book } = useAppSelector((state) => state.books);
  const [layout, setLayout] = useState<ModalDialogProps["layout"] | undefined>(
    undefined
  );
  const tableItems = [
    {
      avatar:
        "https://images.unsplash.com/photo-1511485977113-f34c92461ad9?ixlib=rb-1.2.1&q=80&fm=jpg&crop=faces&fit=crop&h=200&w=200&ixid=eyJhcHBfaWQiOjE3Nzg0fQ",
      name: "Liam James",
      email: "liamjames@example.com",
      phone_nimber: "+1 (555) 000-000",
      position: "Software engineer",
      salary: "$100K",
    },
    {
      avatar: "https://randomuser.me/api/portraits/men/86.jpg",
      name: "Olivia Emma",
      email: "oliviaemma@example.com",
      phone_nimber: "+1 (555) 000-000",
      position: "Product designer",
      salary: "$90K",
    },
    {
      avatar: "https://randomuser.me/api/portraits/women/79.jpg",
      name: "William Benjamin",
      email: "william.benjamin@example.com",
      phone_nimber: "+1 (555) 000-000",
      position: "Front-end developer",
      salary: "$80K",
    },
    {
      avatar: "https://api.uifaces.co/our-content/donated/xZ4wg2Xj.jpg",
      name: "Henry Theodore",
      email: "henrytheodore@example.com",
      phone_nimber: "+1 (555) 000-000",
      position: "Laravel engineer",
      salary: "$120K",
    },
    {
      avatar:
        "https://images.unsplash.com/photo-1439911767590-c724b615299d?ixlib=rb-1.2.1&q=80&fm=jpg&crop=faces&fit=crop&h=200&w=200&ixid=eyJhcHBfaWQiOjE3Nzg0fQ",
      name: "Amelia Elijah",
      email: "amelia.elijah@example.com",
      phone_nimber: "+1 (555) 000-000",
      position: "Open source manager",
      salary: "$75K",
    },
  ];

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
    defaultValues: {},
  });
  const bookGenres = [
    {
      value: "Novel",
      text: "Novel",
    },
    {
      value: "ResearchPaper",
      text: "Research Paper",
    },
    {
      value: "Fiction",
      text: "Fiction",
    },

    {
      value: "TextBook",
      text: "Text Book",
    },
    {
      value: "Other",
      text: "Other",
    },
  ];
  const handleChange = (
    _event: React.SyntheticEvent | null,
    newValue: string | null
  ) => {
    setValue("genre", newValue as string);
  };
  const pageCount = Math.ceil(tableItems.length / itemsPerPage);
  const offset = currentPage * itemsPerPage;
  const currentItems = books.slice(offset, offset + itemsPerPage);
  const [id, setid] = useState("");
  const handlePageClick = ({ selected }: { selected: number }) => {
    setCurrentPage(selected);
  };
  const dispatch = useAppDispatch();

  const handleDelete = (id: string) => {
    dispatch(deleteBook(id));
  };

  useEffect(() => {
    reset({
      title: book?.title,
      description: book?.description,
      genre: book?.genre,
      inventoryCount: book?.inventoryCount,
      isbn: book?.isbn,
      pageCount: book?.pageCount,
      publishedYear: Number(book?.publishedYear),
      id,
    });

    setauthor(book?.author as string[]);
    setimages(book?.images as string[]);
  }, [book]);

  const onsubmit = handleSubmit(async (data) => {
    if (images.length < 4) {
      toast.error("please add atleast 4 images");
      return;
    }
    if (author.length < 1) {
      toast.error("please add atleast 1 book author");
      return;
    }

    dispatch(
      editBook({
        ...data,
        images,
        author,
        publishedYear: data.publishedYear.toString(),
      })
    );
    reset();
    setimages([]);
    setauthor([]);
    setLayout(undefined);
  });

  console.log(errors);

  return (
    <div className="max-w-screen-xl mx-auto px-4 md:px-8">
      <div className="items-start justify-between md:flex">
        <div className="max-w-lg">
          <h3 className="text-gray-800 text-xl font-bold sm:text-2xl">
            All Books
          </h3>
        </div>
        <div className="mt-3 md:mt-0">
          <Link
            to="/admin/create-book"
            className="inline-block px-4 py-2 text-white duration-150 font-medium bg-indigo-600 rounded-lg hover:bg-indigo-500 active:bg-indigo-700 md:text-sm"
          >
            Add Book
          </Link>
        </div>
      </div>
      <div className="mt-12 shadow-sm border rounded-lg overflow-x-auto">
        <table className="w-full table-auto text-sm text-left">
          <thead className="bg-gray-50 text-gray-600 font-medium border-b">
            <tr>
              <th className="py-3 px-6">Title</th>
              <th className="py-3 px-6">Genre</th>
              <th className="py-3 px-6">Author</th>
              <th className="py-3 px-6">Year Published</th>
              <th className="py-3 px-6"></th>
            </tr>
          </thead>
          <tbody className="text-gray-600 divide-y">
            {currentItems.map((item, idx) => (
              <tr key={idx}>
                <td className="flex items-center gap-x-3 py-3 px-6 whitespace-nowrap">
                  <img
                    src={item.images[0]}
                    className="w-10 h-10 rounded-full"
                  />
                  <div>
                    <span className="block text-gray-700 text-sm font-medium">
                      {item.title}
                    </span>
                  </div>
                </td>
                <td className="px-6 py-4 whitespace-nowrap">{item.genre}</td>
                <td className="px-6 py-4 whitespace-nowrap">
                  {new Intl.ListFormat("en-US", {
                    style: "long",
                    type: "conjunction",
                  }).format(item.author)}
                </td>
                <td className="px-6 py-4 whitespace-nowrap">
                  {item.publishedYear}
                </td>
                <td className="text-right px-6 whitespace-nowrap">
                  <Button
                    variant="outlined"
                    color="warning"
                    onClick={() => {
                      setid(item.id as string);
                      dispatch(getBook(item.id as string));
                      setLayout("fullscreen");
                    }}
                  >
                    Edit
                  </Button>
                  <button
                    className="py-2 leading-none px-3 font-medium text-red-600 hover:text-red-500 duration-150 hover:bg-gray-50 rounded-lg"
                    onClick={() => handleDelete(item.id as string)}
                  >
                    Delete
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
        <div className="mt-4 flex justify-center">
          <ReactPaginate
            previousLabel={"<<<"}
            nextLabel={">>>"}
            breakLabel={"..."}
            breakClassName={"break-me"}
            pageCount={pageCount}
            marginPagesDisplayed={1}
            pageRangeDisplayed={5}
            onPageChange={handlePageClick}
            containerClassName={"pagination flex gap-3 w-full justify-end p-4"}
            activeClassName={"active"}
          />
        </div>
      </div>

      <>
        <Modal open={!!layout} onClose={() => setLayout(undefined)}>
          <ModalDialog
            aria-labelledby="layout-modal-title"
            aria-describedby="layout-modal-description"
            layout={layout}
            sx={{ overflowY: "auto" }}
          >
            <ModalClose />

            {book ? (
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
                      <CustomAuthorInput
                        setvalues={setauthor}
                        values={author}
                      />
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
                          defaultValue={book?.genre}
                          onChange={handleChange}
                          sx={{ minWidth: "100%" }}
                        >
                          {bookGenres.map((genre) => (
                            <Option value={genre.value} key={genre.value}>
                              {genre.text}
                            </Option>
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
                        Update
                      </button>
                    </div>
                  </form>
                </div>
              </div>
            ) : (
              <div className=" grid place-items-center h-screen">
                <Loader />
              </div>
            )}
          </ModalDialog>
        </Modal>
      </>
    </div>
  );
};

export default Books;
