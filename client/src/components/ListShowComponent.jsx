import React, { Component } from 'react'
import ShowService from '../services/ShowService'

class ListShowComponent extends Component {
    constructor(props) {
        super(props)

        this.state = {
                shows: []
        }
        this.addShow = this.addShow.bind(this);
        this.editShow = this.editShow.bind(this);
        this.deleteShow = this.deleteShow.bind(this);
    }

    deleteShow(id){
        ShowService.deleteShow(id).then( res => {
            this.setState({shows: this.state.shows.filter(show => show.id !== id)});
        });
    }
    viewShow(id){
        this.props.history.push(`/view-show/${id}`);
    }
    editShow(id){
        this.props.history.push(`/add-show/${id}`);
    }

  componentDidMount(){
        ShowService.getShows().then((res) => {
            if(res.data==null)
            {
                this.props.history.push('/add-show/_add');
            }
            this.setState({ shows: res.data});
        });
    }

    addShow(){
        this.props.history.push('/add-show/_add');
    }

    render() {
        return (
            <div>
                 <h2 className="text-center">Shows List</h2>
                 <div className = "row">
                    <button className="btn btn-primary" onClick={this.addShow}> Add Show</button>
                 </div>
                 <br></br>
                 <div className = "row">
                        <table className = "table table-striped table-bordered">

                            <thead>
                                <tr>
                                    <th> Type</th>
                                    <th> Title</th>
                                    <th> Director</th>
                                    <th> Country</th>
                                    <th> Date Added</th>
                                    <th> Release Year</th>
                                    <th> Rating</th>
                                    <th> Duration</th>
                                    <th> Listed In</th>
                                    <th> Actions</th>
                                </tr>
                            </thead>
                            <tbody>
                                {
                                    this.state.shows.map(
                                        show => 
                                        <tr key = {show.id}>
                                            <td> {show.type}</td>
                                            <td> {show.title}</td>
                                            <td> {show.director}</td>
                                            <td> {show.country}</td>
                                            <td> {show.dateAdded}</td>
                                            <td> {show.releaseYear}</td>
                                            <td> {show.rating}</td>
                                            <td> {show.duration}</td>
                                            <td> {show.listedIn}</td>
                                             <td>
                                                 <button onClick={ () => this.editShow(show.id)} className="btn btn-info">Update </button>
                                                 <button style={{marginLeft: "10px"}} onClick={ () => this.deleteShow(show.id)} className="btn btn-danger">Delete </button>
                                                 <button style={{marginLeft: "10px"}} onClick={ () => this.viewShow(show.id)} className="btn btn-info">View </button>
                                             </td>
                                        </tr>
                                    )
                                }
                            </tbody>
                        </table>

                 </div>

            </div>
        )
    }
}

export default ListShowComponent
