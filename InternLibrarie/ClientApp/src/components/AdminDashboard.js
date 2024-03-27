import React, { useState, useEffect } from 'react';
import logo from "./logo.jpg";
import 'bootstrap/dist/css/bootstrap.min.css';
import './AdminDashboard.css';

const AdminDashboard = ({ onSignOut }) => {
    const [userInputs, setUserInputs] = useState({
        username: '',
        password: '',
        email: '',
        subscriptionType: 'normal'
    });
    const [bookInputs, setBookInputs] = useState({
        title: '',
        author: '',
        genre: '',
        total_copies: ''
    });

    const [editBookTitle, setEditBookTitle] = useState('');
    const [Error, setError] = useState(false);
    const [ErrorText, setErrorText] = useState('');
    const [Errorb, setErrorb] = useState(false);
    const [ErrorTextb, setErrorTextb] = useState('');
    const [SuccessMessage, setSuccessMessage] = useState('');
    const [books, setBooks] = useState([]);

    const fetchBooks = async () => {
        try {
            const userId = 2;
            const response = await fetch(`https://localhost:7231/api/Book/all?userId=${userId}`);
            if (!response.ok) {
                throw new Error('Failed to fetch books');
            }
            const bookData = await response.json();
            setBooks(bookData);

        } catch (error) {
            console.error("Error fetching books:", error);

        }
    };



    const handleUserInputChange = (e) => {
        setUserInputs({ ...userInputs, [e.target.name]: e.target.value });
    };

    const handleBookInputChange = (e) => {
        setBookInputs({ ...bookInputs, [e.target.name]: e.target.value });
    };

    const handleEditBookInputChange = (e) => {
        setEditBookTitle(e.target.value);
    };

    function isValidEmail(email) {

        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

        return emailRegex.test(email);
    }

    const handleAddUser = async () => {
        const { username, password, email, subscriptionType } = userInputs;

        if (!isValidEmail(email)) {
            setError(true);
            setErrorText("Invalid email");
            return;
        } else if (username.length < 5 || username.length > 10) {
            setError(true);
            setErrorText("Username is too short/long");
            return;
        } else if (password.length < 5 || password.length > 10) {
            setError(true);
            setErrorText("Password is too short/long");
            return;
        }


        const response = await fetch('https://localhost:7231/api/User/register', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(userInputs)
        });


        if (!response.ok) {
            if (response.status === 400) {
                setError(true);
                setErrorText('User already exists');
                setSuccessMessage('');
            }
        } else {
            setError(false);
            setErrorText('');
            setSuccessMessage('User added successfully');
            setUserInputs({
                username: '',
                password: '',
                email: '',
                subscriptionType: 'normal'
            });
        }
    }






    const handleDeleteUser = async () => {
        try {
            const response = await fetch(`https://localhost:7231/api/User/${encodeURIComponent(userInputs.username)}`, {
                method: 'DELETE'
            });

            if (response.ok) {
                setError(false);
                setErrorText('User deleted successfully.');
            } else {
                setError(true);
                setErrorText('Failed to delete user.');
            }
        } catch (error) {
            setError(true);
            setErrorText('Failed to delete user.');
            console.error('Error deleting user:', error);
        }
    };




    const handleAddBook = async () => {
        const { title, author, genre, total_copies } = bookInputs;

        if (title.trim() === '') {
            setErrorb(true);
            setErrorTextb("Title is required");
            return;
        } else if (author.trim() === '') {
            setErrorb(true);
            setErrorTextb("Author is required");
            return;
        } else if (genre.trim() === '') {
            setErrorb(true);
            setErrorTextb("Genre is required");
            return;
        } else if (total_copies <= 0) {
            setErrorb(true);
            setErrorTextb("Total Copies must be greater than 0");
            return;
        }

        try {
            const response = await fetch('https://localhost:7231/api/Book/add', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(bookInputs)
            });
            if (response.ok) {
                setErrorb(false);
                setErrorTextb('Book deleted successfully.');
                fetchBooks();
                fetchBooks();
            } else {
                setErrorb(true);
                setErrorTextb('Failed to delete book.');
            }
        } catch (error) {
            setErrorb(true);
            setErrorTextb('Failed to delete book.');
            console.error('Error deleting book:', error);
        }
    };


    const handleEditBook = async () => {
        try {
            const { title, author, genre, total_copies } = bookInputs;

            const updatedBookDto = {
                title: title,
                author: author,
                genre: genre,
                total_copies: total_copies,
            };

            const response = await fetch(`https://localhost:7231/api/Book/edit/${encodeURIComponent(editBookTitle)}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(updatedBookDto)
            });

            if (response.ok) {
                setErrorb(false);
                setErrorTextb('Book edited successfully.');
                fetchBooks();
            } else {
                setErrorb(true);
                setErrorTextb('Failed to edit book.');
            }
        } catch (error) {
            setErrorb(true);
            setErrorTextb('Failed to edit book.');
            console.error('Error editing book:', error);
        }
    };



    const handleDeleteBook = async () => {
        try {
            const response = await fetch(`https://localhost:7231/api/Book/${encodeURIComponent(bookInputs.title)}`, {
                method: 'DELETE'
            });

            if (response.ok) {
                setErrorb(false);
                setErrorTextb('Book deleted successfully.');
            } else {
                setErrorb(true);
                setErrorTextb('Failed to delete book.');
            }
        } catch (error) {
            setErrorb(true);
            setErrorTextb('Failed to delete book.');
            console.error('Error deleting book:', error);
        }
    };


    const handleSignOutClick = () => {
        onSignOut();
    };


    const [showBookSection, setShowBookSection] = useState(false);

    const handleShowBookSection = (e) => {
        e.preventDefault();
        setShowBookSection(true);
    };

    const handleShowUserSection = (e) => {
        e.preventDefault();
        setShowBookSection(false);
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
                                <a className="nav-link active" aria-current="page" href='/' onClick={handleShowUserSection} style={{ fontWeight: 'bold', color: 'white' }}>Users</a>
                            </li>
                            <li className="nav-item">
                                <a className="nav-link" onClick={handleShowBookSection} href='/' style={{ fontWeight: 'bold', color: 'white' }}>Books</a>
                            </li>
                        </ul>
                        <form className="d-flex" onSubmit={handleSignOutClick}>
                            <button className="btn btn-primary btn-primary-custom" type="submit">Sign Out</button>
                        </form>
                    </div>
                </div>
            </nav>

            <h1 className="dashboard-title" style={{ marginTop: '20px' }}>Dashboard</h1>

            {showBookSection ? (
                <BookSection handleAddBook={handleAddBook} handleEditBook={handleEditBook} handleDeleteBook={handleDeleteBook} bookInputs={bookInputs} handleBookInputChange={handleBookInputChange} editBookTitle={editBookTitle} handleEditBookInputChange={handleEditBookInputChange} Errorb={Errorb} ErrorTextb={ErrorTextb} />
            ) : (
                <UserSection handleAddUser={handleAddUser} handleDeleteUser={handleDeleteUser} userInputs={userInputs} handleUserInputChange={handleUserInputChange} Error={Error} ErrorText={ErrorText} SuccessMessage={SuccessMessage} />
            )}
        </div>
    );
};

const UserSection = ({ handleAddUser, handleDeleteUser, userInputs, handleUserInputChange, Error, ErrorText, SuccessMessage }) => {
    return (
        <div className="admin-dashboard-container">
            <div className="user-section-container">
                <div className="user-section">
                    <h2>User Section</h2>
                    <input
                        type="text"
                        placeholder="Username"
                        name="username"
                        value={userInputs.username}
                        onChange={handleUserInputChange}
                    />
                    <input
                        type="password"
                        placeholder="Password"
                        name="password"
                        value={userInputs.password}
                        onChange={handleUserInputChange}
                    />
                    <input
                        type="email"
                        placeholder="Email"
                        name="email"
                        value={userInputs.email}
                        onChange={handleUserInputChange}
                    />
                    <select
                        name="subscriptionType"
                        value={userInputs.subscriptionType}
                        onChange={handleUserInputChange}
                    >
                        <option value="normal">Normal</option>
                        <option value="premium">Premium</option>
                    </select>
                    <div>
                        <button onClick={handleAddUser} style={{ marginRight: '10px' }}>Add User</button>
                        <button onClick={handleDeleteUser} style={{ marginRight: '10px' }}>Delete User</button>
                    </div>
                    {Error && <p className="error-text">{ErrorText}</p>}
                    <p>{SuccessMessage}</p>
                </div>
            </div>
        </div>
    );
};

const BookSection = ({ handleAddBook, handleEditBook, handleDeleteBook, bookInputs, handleBookInputChange, editBookTitle, handleEditBookInputChange, Errorb, ErrorTextb }) => {

    const [books, setBooks] = useState([]);
    useEffect(() => {
        fetchBooks();
    }, []);
    const fetchBooks = async () => {
        try {
            const userId = 1037;
            const response = await fetch(`https://localhost:7231/api/Book/all?userId=${userId}`);
            if (!response.ok) {
                throw new Error('Failed to fetch books');
            }
            const bookData = await response.json();
            setBooks(bookData);

        } catch (error) {
            console.error("Error fetching books:", error);

        }
    };
    return (
        <div className="admin-dashboard-container">
            <div className="book-section-container">
                <div className="book-section">
                    <h2>Book Section</h2>
                    <input
                        type="text"
                        placeholder="Title"
                        name="title"
                        value={bookInputs.title}
                        onChange={handleBookInputChange}
                    />
                    <input
                        type="text"
                        placeholder="Author"
                        name="author"
                        value={bookInputs.author}
                        onChange={handleBookInputChange}
                    />
                    <input
                        type="text"
                        placeholder="Genre"
                        name="genre"
                        value={bookInputs.genre}
                        onChange={handleBookInputChange}
                    />
                    <input
                        type="number"
                        placeholder="Total Copies"
                        name="total_copies"
                        value={bookInputs.total_copies}
                        onChange={handleBookInputChange}
                    />
                    <input
                        type="text"
                        placeholder="Enter Book Title to Edit"
                        value={editBookTitle}
                        onChange={handleEditBookInputChange}
                    />
                    <div>
                        <button onClick={handleAddBook} style={{ marginRight: '10px' }}>Add Book</button>
                        <button onClick={handleEditBook} style={{ marginRight: '10px' }}>Edit Book</button>
                        <button onClick={handleDeleteBook}>Delete Book</button>
                    </div>
                    
                    {Errorb && <p className="error-text">{ErrorTextb}</p>}


                    <table className="book-table">
                        <thead>
                            <tr>
                                <th>Title</th>
                                <th>Author</th>
                                <th>Genre</th>
                                <th>Total Copies</th>
                            </tr>
                        </thead>
                        <tbody>
                            {books.map((book, index) => (
                                <tr key={index}>
                                    <td>{book.title}</td>
                                    <td>{book.author}</td>
                                    <td>{book.genre}</td>
                                    <td>{book.total_copies}</td>
                                </tr>
                            ))}
                        </tbody>
                    </table>

                </div>
            </div>
        </div>
    );
};

export default AdminDashboard;
