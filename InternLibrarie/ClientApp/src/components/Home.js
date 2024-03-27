import React, { useState, useEffect } from 'react';
import './Home.css';
import logo from "./logo.jpg";
import 'bootstrap/dist/css/bootstrap.min.css';
import book1Image from "./book1.jpeg";

const Home = ({ userId, onSignOut, navigateToProfile }) => {
    const [searchTerm, setSearchTerm] = useState("");
    const [books, setBooks] = useState([]);
    const [errorMessage, setErrorMessage] = useState("");

    const fetchBooks = async () => {
        try {
            const response = await fetch(`https://localhost:7231/api/Book/all?userId=${userId}`);
            if (!response.ok) {
                throw new Error('Failed to fetch books');
            }
            const bookData = await response.json();
            setBooks(bookData);
            setErrorMessage("");
        } catch (error) {
            console.error("Error fetching books:", error);
            setErrorMessage("Failed to fetch books");
        }
    };

    useEffect(() => {
        fetchBooks();
    }, [userId]);

    const handleSearch = async (e) => {
        e.preventDefault();
        try {
            const response = await fetch(`https://localhost:7231/api/Book/search?userId=${userId}&searchTerm=${searchTerm}`);
            const searchData = await response.json();
            const searchedBook = searchData.find(book => book.title.toLowerCase() === searchTerm.toLowerCase());

            if (searchedBook) {
                const filteredBooks = books.filter(book => book.booksId !== searchedBook.booksId);
                const updatedBooks = [searchedBook, ...filteredBooks];
                setBooks(updatedBooks);
            }
        } catch (error) {
            console.error("Error searching books:", error);
        }
    };

    const handleBorrowBook = async (bookId) => {
        try {
            const response = await fetch(`https://localhost:7231/api/Book/borrow?userId=${userId}&bookId=${bookId}`, {
                method: 'POST'
            });
            if (!response.ok) {
                const errorMessage = await response.text();
                throw new Error(errorMessage);
            }

            const updatedBookResponse = await fetch(`https://localhost:7231/api/Book/${bookId}`);
            if (!updatedBookResponse.ok) {
                throw new Error('Failed to fetch updated book data');
            }
            const updatedBook = await updatedBookResponse.json();

            const updatedBooks = books.map(book => {
                if (book.booksId === bookId) {
                    return { ...book, isBorrowed: true, total_copies: updatedBook.total_copies };
                }
                return book;
            });
            setBooks(updatedBooks);
        } catch (error) {
            console.error("Error borrowing book:", error);
            setErrorMessage(error.message);
        }
    };

    const handleReturnBook = async (bookId) => {
        try {
            const response = await fetch(`https://localhost:7231/api/Book/return?userId=${userId}&bookId=${bookId}`, {
                method: 'POST'
            });
            if (!response.ok) {
                const errorMessage = await response.text();
                throw new Error(errorMessage);
            }

            const updatedBookResponse = await fetch(`https://localhost:7231/api/Book/${bookId}`);
            if (!updatedBookResponse.ok) {
                throw new Error('Failed to fetch updated book data');
            }
            const updatedBook = await updatedBookResponse.json();

            const updatedBooks = books.map(book => {
                if (book.booksId === bookId) {
                    return { ...book, isBorrowed: false, total_copies: updatedBook.total_copies };
                }
                return book;
            });
            setBooks(updatedBooks);
        } catch (error) {
            console.error("Error returning book:", error);
            setErrorMessage(error.message);
        }
    };

    const handleProfileClick = (e) => {
        e.preventDefault();
        navigateToProfile()
    };
    const handleSignOutClick = () => {
        onSignOut();
    };

    return (
        <div>
            <nav className="navbar navbar-expand-lg navbar-light" style={{ backgroundColor: 'blue' }}>
                <div className="container-fluid">
                    <img src={logo} alt="Logo" className="logo" height={10} style={{ marginRight: '30px' }} />

                    <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarScroll" aria-controls="navbarScroll" aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="collapse navbar-collapse" id="navbarScroll">
                        <ul className="navbar-nav me-auto my-2 my-lg-0 navbar-nav-scroll" style={{ '--bs-scroll-height': '100px' }}>
                            <li className="nav-item">
                                <a className="nav-link active" aria-current="page" href='/' onClick={handleProfileClick} style={{ fontWeight: 'bold', color: 'white' }}>Home</a>
                            </li>
                            <li className="nav-item">
                                <a className="nav-link" onClick={handleProfileClick} href='/' style={{ fontWeight: 'bold', color: 'white' }}>Profile</a>
                            </li>
                        </ul>
                        <form className="d-flex" onSubmit={handleSignOutClick}>
                            <button className="btn btn-primary btn-primary-custom" type="submit">Sign Out</button>
                        </form>
                    </div>
                </div>
            </nav>

            <div style={{ display: 'flex', justifyContent: 'center', marginTop: '20px' }}>
                <form className="d-flex" onClick={handleSearch}>
                    <input className="form-control me-2" type="search" aria-label="Search"
                        placeholder="Search for books..."
                        value={searchTerm}
                        onChange={(e) => setSearchTerm(e.target.value)} />
                    <button className="btn btn-primary btn-primary-custom" onClick={handleSearch}>Search</button>
                </form>
            </div>

            <div className="main-container">
                <div className="books-container">
                    {books.map(book => (
                        <div className="card" key={book.booksId}>
                            <img src={book1Image} alt="Book" />
                            <div className="card-details">
                                <h3>{book.title}</h3>
                                <p>{book.author}</p>
                                <p>Copies available: {book.total_copies}</p>
                                <div className="buttons">
                                    {!book.isBorrowed && <button onClick={() => handleBorrowBook(book.booksId)}>Borrow Book</button>}
                                    {book.isBorrowed && <button onClick={() => handleReturnBook(book.booksId)}>Return Book</button>}
                                    <div style={{ marginBottom: '10px' }}></div>
                                </div>
                            </div>
                        </div>
                    ))}
                </div>
            </div>

            {errorMessage && (
                <div className="popup">
                    <p>{errorMessage}</p>
                    <button onClick={() => setErrorMessage("")}>Close</button>
                </div>
            )}
        </div>
    );
};

export default Home;

