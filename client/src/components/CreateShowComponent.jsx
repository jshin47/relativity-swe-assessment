import React, { Component } from "react";
import ShowService from "../services/ShowService";
import ShowCategoryService from "../services/ShowCategoryService";
import DatePicker from "react-datepicker";
import CreatableSelect from "react-select/creatable";

import "react-datepicker/dist/react-datepicker.css";

class CreateShowComponent extends Component {
  constructor(props) {
    super(props);
    this.state = {
      id: this.props.match.params.id,

      showId: "",
      type: "Movie",
      title: "",
      director: "",
      country: "",
      dateAdded: "",
      releaseYear: "",
      rating: "",
      duration: "",
      allShowCategories: [],
      categories: [],
    };
    this.saveOrUpdateShow = this.saveOrUpdateShow.bind(this);
  }
  0;
  componentDidMount() {
    // Retrieve the list of categories

    ShowCategoryService.getShowCategories().then((res) => {
      this.setState({
        allShowCategories: res.data.map((x) => ({ value: x, label: x })),
      });
    });

    if (this.state.id === "_add") {
      return;
    } else {
      ShowService.getShowById(this.state.id).then((res) => {
        let show = res.data;
        console.log(show);
        show.dateAdded = new Date(show.dateAdded);
        this.setState(show);
      });
    }
  }

  saveOrUpdateShow = (e) => {
    e.preventDefault();

    let show = {
      showId: this.state.showId,
      type: this.state.type,
      title: this.state.title,
      director: this.state.director,
      country: this.state.country,
      dateAdded: this.state.dateAdded.toISOString().split("T")[0],
      releaseYear: this.state.releaseYear,
      rating: this.state.rating,
      duration: this.state.duration,
      categories: this.state.categories,
    };

    if (this.state.id === "_add") {
      ShowService.createShow(show).then(
        (res) => {
          this.props.history.push("/shows");
        },
        (err) => this.setState({ errorMessage: err.message })
      );
    } else {
      ShowService.updateShow(show, this.state.id).then(
        (res) => {
          this.props.history.push("/shows");
        },
        (err) => this.setState({ errorMessage: err.message })
      );
    }
  };

  cancel() {
    this.props.history.push("/shows");
  }

  getTitle() {
    if (this.state.id === "_add") {
      return <h3 className="text-center">Add Show</h3>;
    } else {
      return <h3 className="text-center">Update Show</h3>;
    }
  }
  render() {
    return (
      <div>
        <br></br>
        <div className="container">
          <div className="row">
            <div className="card col-md-6 offset-md-3 offset-md-3">
              {this.getTitle()}
              <div className="card-body">
                <form>
                  <div className="form-group">
                    <label> Type: </label>
                    <select
                      name="type"
                      className="form-control"
                      value={this.state.type}
                      onChange={(e) => this.setState({ type: e.target.value })}
                    >
                      <option>Movie</option>
                      <option>TV Show</option>
                    </select>
                  </div>
                  <div className="form-group">
                    <label> Title: </label>
                    <input
                      placeholder="Title"
                      name="title"
                      className="form-control"
                      value={this.state.title}
                      onChange={(e) => this.setState({ title: e.target.value })}
                    />
                  </div>
                  <div className="form-group">
                    <label> Director: </label>
                    <input
                      placeholder="Director"
                      name="director"
                      className="form-control"
                      value={this.state.director}
                      onChange={(e) =>
                        this.setState({ director: e.target.value })
                      }
                    />
                  </div>
                  <div className="form-group">
                    <label> Country: </label>
                    <input
                      placeholder="Country"
                      name="country"
                      className="form-control"
                      value={this.state.country}
                      onChange={(e) =>
                        this.setState({ country: e.target.value })
                      }
                    />
                  </div>
                  <div className="form-group">
                    <label> Date Added: </label>
                    <DatePicker
                      className="form-control"
                      selected={this.state.dateAdded}
                      onChange={(date) => this.setState({ dateAdded: date })}
                    />
                  </div>
                  <div className="form-group">
                    <label> Release Year: </label>
                    <input
                      placeholder="Release Year"
                      name="releaseYear"
                      className="form-control"
                      value={this.state.releaseYear}
                      onChange={(e) =>
                        this.setState({ releaseYear: e.target.value })
                      }
                    />
                  </div>
                  <div className="form-group">
                    <label> Rating: </label>
                    <input
                      placeholder="Rating"
                      name="rating"
                      className="form-control"
                      value={this.state.rating}
                      onChange={(e) =>
                        this.setState({ rating: e.target.value })
                      }
                    />
                  </div>
                  <div className="form-group">
                    <label> Duration: </label>
                    <input
                      placeholder="Duration"
                      name="duration"
                      className="form-control"
                      value={this.state.duration}
                      onChange={(e) =>
                        this.setState({ duration: e.target.value })
                      }
                    />
                  </div>
                  <div className="form-group">
                    <label> Categories: </label>
                    <CreatableSelect
                      isMulti
                      name="categories"
                      isClearable
                      options={this.state.allShowCategories}
                      className="basic-multi-select"
                      classNamePrefix="select"
                      value={this.state.categories.map((x) => ({
                        label: x,
                        value: x,
                      }))}
                      onChange={(v) =>
                        this.setState({
                          categories: Array.from(v).map((x) => x.value),
                        })
                      }
                    />
                  </div>
                  <button
                    className="btn btn-success"
                    onClick={this.saveOrUpdateShow}
                  >
                    Save
                  </button>
                  <button
                    className="btn btn-danger"
                    onClick={this.cancel.bind(this)}
                    style={{ marginLeft: "10px" }}
                  >
                    Cancel
                  </button>

                  {this.state.errorMessage && (
                    <h5 className="alert alert-danger">
                      {this.state.errorMessage}{" "}
                    </h5>
                  )}
                </form>
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }
}

export default CreateShowComponent;
