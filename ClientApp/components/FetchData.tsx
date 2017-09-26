import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

interface FetchDataExampleState {
    plugins: DbPlugin[];
    loading: boolean;
}

export class FetchData extends React.Component<RouteComponentProps<{}>, FetchDataExampleState> {
    constructor() {
        super();
        this.state = { plugins: [], loading: true };

        fetch('api/SampleData/DbQuery')
            .then(response => response.json() as Promise<DbPlugin[]>)
            .then(data => {
                this.setState({ plugins: data, loading: false });
            });
    }

    public render() {
        return <div>
            <h1>What plugins are running on the DB server?</h1>
            <p>This code hits some C# logic on the server. It won't work if you are offline.</p>
            { this.state.loading ? <p><em>Loading...</em></p> : (
                <table className='table'>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Description</th>
                        </tr>
                    </thead>
                    <tbody>
                    {this.state.plugins.map(plugin =>
                        <tr key={ plugin.name }>
                            <td>{ plugin.name }</td>
                            <td>{ plugin.description }</td>
                        </tr>
                    )}
                    </tbody>
                </table>
            )}
        </div>
    }
}

interface DbPlugin {
    name: string,
    description: string,
}
