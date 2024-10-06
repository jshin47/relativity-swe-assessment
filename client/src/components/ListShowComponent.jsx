import React, { Component } from "react";
import ShowService from "../services/ShowService";
import {
  DatatableWrapper,
  Filter,
  Pagination,
  PaginationOptions,
  BulkCheckboxControl,
  TableBody,
  TableHeader,
} from "react-bs-datatable";
import { Col, Row, Table } from "react-bootstrap";

class ListShowComponent extends Component {
  constructor(props) {
    super(props);

    this.state = {
      shows: [],
    };
    this.addShow = this.addShow.bind(this);
    this.editShow = this.editShow.bind(this);
    this.deleteShow = this.deleteShow.bind(this);
  }

  deleteShow(id) {
    ShowService.deleteShow(id).then((res) => {
      this.getShows(this.state.pageIndex);
    });
  }
  editShow(id) {
    this.props.history.push(`/add-show/${id}`);
  }

  componentDidMount() {
    this.getShows(1);
  }

  getShows(pageIndex = this.state.pageIndex) {
    ShowService.getShows(pageIndex).then((res) => {
      if (res.data == null) {
        this.props.history.push("/add-show/_add");
      }
      this.setState({ 
        shows: res.data.items,
        pageIndex: res.data.pageIndex,
        totalPages: res.data.totalPages,
      });
    });
  }

  addShow() {
    this.props.history.push("/add-show/_add");
  }

  render() {
    return (
      <div>
        <div className="row" style={{ marginTop: "20px" }}>
          <div className="col-md-4">
            <button className="btn btn-primary" onClick={this.addShow}>
              Add Show
            </button>
          </div>
          <div className="col-md-4">
            <h2 className="text-center">Shows List</h2>
          </div>
          <div className="col-md-4"></div>
        </div>
        <DatatableWrapper
          body={this.state.shows}
          headers={[
            {
              prop: "type",
              title: "Type",
              isSortable: true,
              isFilterable: true,
            },
            {
              prop: "title",
              title: "Title",
              isSortable: true,
              isFilterable: true,
            },
            {
              prop: "director",
              title: "Director",
              isSortable: true,
              isFilterable: true,
            },
            {
              prop: "country",
              title: "Country",
              isSortable: true,
              isFilterable: true,
            },
            {
              prop: "dateAdded",
              title: "Date Added",
              isSortable: true,
              isFilterable: true,
            },
            {
              prop: "releaseYear",
              title: "Release Year",
              isSortable: true,
              isFilterable: true,
            },
            {
              prop: "duration",
              title: "Duration",
              isSortable: true,
              isFilterable: true,
            },
            {
              prop: "categories",
              title: "Listed In",
              isSortable: true,
              isFilterable: true,
              cell: (row) => (
                <span>{row.categories.join(', ')}</span>
              ),
            },
            {
              prop: "actions",
              title: 'Movie Actions',
              thProps: {
                style: {
                  minWidth: '160px',
                }
              },
              cell: (row) => (
                <div>
                  <button
                    style={{ marginLeft: "10px", fontSize: "small", padding: "2px" }}
                    onClick={() => this.editShow(row.id)}
                    className="btn btn-primary"
                  >
                    Edit
                  </button>
                  <button
                    style={{ marginLeft: "10px", fontSize: "small", padding: "2px" }}
                    onClick={() => this.deleteShow(row.id)}
                    className="btn btn-danger"
                  >
                    Del
                  </button>
                </div>
              ),
            },
          ]}
          sortProps={{
            sortValueObj: {
              date: (date) => `${date}`,
            },
          }}
          paginationOptionsProps={{
            initialState: {
              rowsPerPage: 10,
              options: [5, 10, 15, 20],
            },
          }}
        >
          <Row className="mb-4">
            <Col
              xs={12}
              lg={4}
              className="d-flex flex-col justify-content-end align-items-end"
            >
              <Filter />
            </Col>
            <Col
              xs={12}
              sm={6}
              lg={4}
              className="d-flex flex-col justify-content-lg-center align-items-center justify-content-sm-start mb-2 mb-sm-0"
            >
              <PaginationOptions alwaysShowPagination />
            </Col>
            <Col
              xs={12}
              sm={6}
              lg={4}
              className="d-flex flex-col justify-content-end align-items-end"
            >
              <Pagination alwaysShowPagination paginationRange={5} controlledProps={{
                currentPage: this.state.pageIndex,
                maxPage: this.state.totalPages,
                onPaginationChange: (pageIndex) => {
                  this.getShows(pageIndex)
                },
              }} />
            </Col>
            <Col xs={12} className="mt-2">
              <BulkCheckboxControl />
            </Col>
          </Row>
          <Table>
            <TableHeader />
            <TableBody />
          </Table>
        </DatatableWrapper>
      </div>
    );
  }
}

export default ListShowComponent;
