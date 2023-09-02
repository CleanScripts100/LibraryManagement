import { useEffect } from "react"
import { useAppDispatch, useAppSelector } from "../hooks/reduxHooks"
import { useParams } from "react-router-dom"
import { getBook } from "../redux/reducers/bookReducers"

const Book = () => {

    const dispatch = useAppDispatch()
    const { id } = useParams() as { id: string }
    const {book} =  useAppSelector((state)=> state.books)
    useEffect(() => {
       dispatch(getBook(id))

        
    },[])

  return (
    <section className="py-10 font-poppins dark:bg-gray-800">
      <div className="max-w-6xl px-4 mx-auto">
       
          <div className="w- px-4 mb-8 md:w-9/10 md:mb-0">
            <div className="sticky top-0 overflow-hidden ">
              <div className="relative mb-6 lg:mb-10 lg:h-96">
                <a
                  className="absolute left-0 transform lg:ml-2 top-1/2 translate-1/2"
                  href="#"
                >
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    width="16"
                    height="16"
                    fill="currentColor"
                    className="w-5 h-5 text-blue-500 bi bi-chevron-left dark:text-blue-200"
                    viewBox="0 0 16 16"
                  >
                    <path
                      fill-rule="evenodd"
                      d="M11.354 1.646a.5.5 0 0 1 0 .708L5.707 8l5.647 5.646a.5.5 0 0 1-.708.708l-6-6a.5.5 0 0 1 0-.708l6-6a.5.5 0 0 1 .708 0z"
                    ></path>
                  </svg>
                </a>
                <img
                  className="object-contain w-full lg:h-full"
                  src={book?.images[0]}
                  alt=""
                />
                <a
                  className="absolute right-0 transform lg:mr-2 top-1/2 translate-1/2"
                  href="#"
                >
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    width="16"
                    height="16"
                    fill="currentColor"
                    className="w-5 h-5 text-blue-500 bi bi-chevron-right dark:text-blue-200"
                    viewBox="0 0 16 16"
                  >
                    <path
                      fill-rule="evenodd"
                      d="M4.646 1.646a.5.5 0 0 1 .708 0l6 6a.5.5 0 0 1 0 .708l-6 6a.5.5 0 0 1-.708-.708L10.293 8 4.646 2.354a.5.5 0 0 1 0-.708z"
                    ></path>
                  </svg>
                </a>
              </div>
            <div className="flex-wrap hidden -mx-2 md:flex">
              
              {
                book?.images.map(image => {
                  return (
                    <div className="w-1/2 p-2 sm:w-1/4" key={image}>
                      <a
                        className="block border border-gray-200 hover:border-blue-400 dark:border-gray-700 dark:hover:border-blue-300"
                        href="#"
                      >
                        <img
                          className="object-contain w-full lg:h-28"
                          src={image}
                          alt=""
                        />
                      </a>
                    </div>
                  );
     

                })
              }
         
              </div>
            </div>
          </div>

          <div className=" px-4 md:w-full">
            <div className="lg:pl-20">
              <div className="lg:w-9/10 w-full lg:pr-10 lg:py-6 mb-6 lg:mb-0">
               
                <h1 className="text-gray-900 text-3xl title-font font-medium mb-4">
                  {book?.title}
                </h1>
                <div className="flex mb-4">
                  <a className="flex-grow text-blue-500 border-b-2 border-blue-500 py-2 text-lg px-1">
                    Description
                  </a>
                  <a className="flex-grow border-b-2 border-gray-300 py-2 text-lg px-1">
                    Reviews
                  </a>
                  <a className="flex-grow border-b-2 border-gray-300 py-2 text-lg px-1">
                    Details
                  </a>
                </div>
                <p className="leading-relaxed mb-4">{book?.description}</p>
                <div className="flex border-t border-gray-200 py-2">
                  <span className="text-gray-500">Genre</span>
                  <span className="ml-auto text-gray-900">{book?.genre}</span>
                </div>
                <div className="flex border-t border-gray-200 py-2">
                  <span className="text-gray-500">ISBN</span>
                  <span className="ml-auto text-gray-900">{book?.isbn}</span>
                </div>
                <div className="flex border-t border-gray-200 py-2">
                  <span className="text-gray-500">Year Pubished</span>
                  <span className="ml-auto text-gray-900">
                    {book?.publishedYear}
                  </span>
                </div>

                <div className="flex border-t border-gray-200 py-2">
                  <span className="text-gray-500">Number of Pages</span>
                  <span className="ml-auto text-gray-900">
                    {book?.pageCount}
                  </span>
                </div>
                <div className="flex border-t border-gray-200 py-2">
                  <span className="text-gray-500">Number of Inventory</span>
                  <span className="ml-auto text-gray-900">
                    {book?.inventoryCount}
                  </span>
                </div>
                <div className="flex border-t border-b mb-6 border-gray-200 py-2">
                  <span className="text-gray-500">Author</span>
                  <span className="ml-auto text-gray-900">{book?.author}</span>
                </div>

                <div className="flex">
                  <button className=" flex-1 text-white bg-blue-500 border-0 py-2 px-6 focus:outline-none hover:bg-blue-600 rounded">
                    Borrow Book
                  </button>
                  <button className="rounded-full w-10 h-10 bg-gray-200 p-0 border-0 inline-flex items-center justify-center text-gray-500 ml-4">
                    <svg
                      fill="currentColor"
                      stroke-linecap="round"
                      stroke-linejoin="round"
                      stroke-width="2"
                      className="w-5 h-5"
                      viewBox="0 0 24 24"
                    >
                      <path d="M20.84 4.61a5.5 5.5 0 00-7.78 0L12 5.67l-1.06-1.06a5.5 5.5 0 00-7.78 7.78l1.06 1.06L12 21.23l7.78-7.78 1.06-1.06a5.5 5.5 0 000-7.78z"></path>
                    </svg>
                  </button>
                </div>
              </div>
            </div>
          </div>
      </div>
    </section>
  );
}

export default Book