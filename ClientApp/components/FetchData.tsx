import * as React from "react";
import { RouteComponentProps } from "react-router";
import "isomorphic-fetch";

interface FetchDataExampleState {
  plugins: DbPlugin[] | null;
  graphQLResponse: object | null;
}

export class FetchData extends React.Component<
  RouteComponentProps<{}>,
  FetchDataExampleState
> {
  constructor() {
    super();
    this.state = { plugins: null, graphQLResponse: null };

    fetch("api/DbQuery")
      .then(response => response.json() as Promise<DbPlugin[]>)
      .then(data => {
        this.setState({ plugins: data });
      });

    fetch("api/graphQL")
      .then(response => response.json() as Promise<object>)
      .then(data => {
        this.setState({ graphQLResponse: data });
      });
  }

  public render() {
    return (
      <div>
        <h1>Percona Server + GraphQL Test</h1>
        <p>
          This code hits some C# logic on the server. It won't work if you are
          offline.
        </p>
        {this.state.graphQLResponse && (
          <p>GraphQL response: {JSON.stringify(this.state.graphQLResponse)}</p>
        )}
        {this.state.plugins == null ? (
          <p>
            <em>MySQL query loading...</em>
          </p>
        ) : (
          <table className="table">
            <thead>
              <tr>
                <th>Name</th>
                <th>Description</th>
              </tr>
            </thead>
            <tbody>
              {this.state.plugins.map(plugin => (
                <tr key={plugin.name}>
                  <td>{plugin.name}</td>
                  <td>{plugin.description}</td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>
    );
  }
}

interface DbPlugin {
  name: string;
  description: string;
}
