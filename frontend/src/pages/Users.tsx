import React from "react";
import ReactPaginate from "react-paginate";
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";

import { useAppDispatch, useAppSelector } from "../hooks/reduxHooks";
import { deleteUser, getUsers } from "../redux/reducers/userReducers";

const Users = () => {
  const itemsPerPage = 5; // Number of items to display per page
  const [currentPage, setCurrentPage] = useState(0);

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

  const { users, user } = useAppSelector((state) => state.user);
  const dispatch = useAppDispatch();
  console.log(users);

  const pageCount = Math.ceil(tableItems.length / itemsPerPage);
  const offset = currentPage * itemsPerPage;
  const currentItems = users.slice(offset, offset + itemsPerPage);

  const handlePageClick = ({ selected }: { selected: number }) => {
    setCurrentPage(selected);
  };

  useEffect(() => {
    dispatch(getUsers());
  }, []);

  const handleDelete = (id: string) => {
    dispatch(deleteUser(id));
  };

  return (
    <div className="max-w-screen-xl mx-auto px-4 md:px-8">
      <div className="items-start justify-between md:flex">
        <div className="max-w-lg">
          <h3 className="text-gray-800 text-xl font-bold sm:text-2xl">
            All Users
          </h3>
        </div>
        <div className="mt-3 md:mt-0">
          <Link
            to="/admin/create-User"
            className="inline-block px-4 py-2 text-white duration-150 font-medium bg-indigo-600 rounded-lg hover:bg-indigo-500 active:bg-indigo-700 md:text-sm"
          >
            Add User
          </Link>
        </div>
      </div>
      <div className="mt-12 shadow-sm border rounded-lg overflow-x-auto">
        <table className="w-full table-auto text-sm text-left">
          <thead className="bg-gray-50 text-gray-600 font-medium border-b">
            <tr>
              <th className="py-3 px-6">Name</th>
              <th className="py-3 px-6">Email</th>
              <th className="py-3 px-6">Role</th>

              <th className="py-3 px-6"></th>
            </tr>
          </thead>
          <tbody className="text-gray-600 divide-y">
            {currentItems.map((item, idx) => {
              if (item.id !== user?.id) {
                return (
                  <tr key={item.id}>
                    <td className="flex items-center gap-x-3 py-3 px-6 whitespace-nowrap">
                      <img
                        src={item.avatar}
                        className="w-10 h-10 rounded-full"
                      />
                      <div>
                        <span className="block text-gray-700 text-sm font-medium">
                          {item.firstName}
                        </span>
                        {/* <span className="block text-gray-700 text-xs">
                         {item.lastName}
                       </span> */}
                      </div>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap">
                      {item.email}
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap">{item.role}</td>

                    <td className="text-right px-6 whitespace-nowrap">
                      <Link
                        to={`/admin/users/${item?.id}`}
                        className="py-2 px-3 font-medium text-indigo-600 hover:text-indigo-500 duration-150 hover:bg-gray-50 rounded-lg"
                      >
                        Edit
                      </Link>
                      <button
                        className="py-2 leading-none px-3 font-medium text-red-600 hover:text-red-500 duration-150 hover:bg-gray-50 rounded-lg"
                        onClick={() => handleDelete(item.id as string)}
                      >
                        Delete
                      </button>
                    </td>
                  </tr>
                );
              }
            })}
          </tbody>
        </table>
        <div className="mt-4 flex justify-center">
          <ReactPaginate
            previousLabel={"Previous"}
            nextLabel={"Next"}
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
    </div>
  );
};

export default Users;
