import React, { useState, useEffect } from 'react';

import 'bootstrap/dist/css/bootstrap.min.css';
import logo from "./logo.jpg";

const Profile = ({ userId, onSignOut }) => {
    const [bills, setBills] = useState([]);
    const [errorMessage, setErrorMessage] = useState("");

    useEffect(() => {
 
        fetchBills();
    }, [userId]);

    const fetchBills = async () => {
        try {
            const response = await fetch(`https://localhost:7231/api/Bills/bill/${userId}`);
            if (!response.ok) {
                throw new Error('Failed to fetch bills');
            }
            const billsData = await response.json();
            setBills(billsData);
            setErrorMessage("");
        } catch (error) {
            console.error("Error fetching bills:", error);
            setErrorMessage("Failed to fetch bills");
        }
    };

    const handleRenewSubscription = async () => {
        try {
            const response = await fetch(`https://localhost:7231/api/Subscriptions/renew/${userId}`, {
                method: 'POST'
            });
            if (!response.ok) {
                const errorMessage = await response.text();
                throw new Error(errorMessage);
            }
            fetchBills();
        } catch (error) {
            console.error("Error renewing subscription:", error);
            setErrorMessage(error.message);
        }
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
                                <a className="nav-link active" aria-current="page" href='/' style={{ fontWeight: 'bold', color: 'white' }}>Home</a>
                            </li>
                            <li className="nav-item">
                                <a className="nav-link" href='/' style={{ fontWeight: 'bold', color: 'white' }}>Profile</a>
                            </li>
                        </ul>
                        <form className="d-flex" onSubmit={handleSignOutClick}>
                            <button className="btn btn-primary btn-primary-custom" type="submit">Sign Out</button>
                        </form>
                    </div>
                </div>
            </nav>
            
            {errorMessage && <p className="error-message">{errorMessage}</p>}
            {bills.length > 0 ? (
                <table className="table">
                    <thead>
                        <tr>
                            <th>Borrow ID</th>
                            <th>User Name</th>
                            <th>Book Title</th>
                            <th>Borrow Date</th>
                            <th>Return Date</th>
                            <th>Total Fees Due</th>
                        </tr>
                    </thead>
                    <tbody>
                        {bills.map(bill => (
                            <tr key={bill.borrowId}>
                                <td>{bill.borrowId}</td>
                                <td>{bill.userName}</td>
                                <td>{bill.bookTitle}</td>
                                <td>{bill.borrowDate}</td>
                                <td>{bill.returnDate}</td>
                                <td>{bill.totalFeesDue}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>

            ) : (
                <p>No bills found for the user.</p>
            )}
            <button className="btn btn-primary btn-primary-custom" onClick={handleRenewSubscription}>Renew Subscription</button>
        </div>
    );
};

export default Profile;
