# Daylio-Parser


Daylio-Parser is a CLI for accessing [DaylioData](https://github.com/jav76/DaylioData).

## Usage

The executable can be started with command line arguments which will be parsed automatically by the shell. The running CLI will parse any input as commands as if they were command line arguments continually.

### Commmands

#### Summary

The `summary` command gives a summary string of concatenated DaylioData summary properties with `[SummaryPropertyAttribute]`

Example:

```summary --file <path_to_daylio_export.csv>```

Required Arguments:

- `--file <path_to_daylio_export.csv>`: Specifies the path to the Daylio CSV export file. If any other file path has already been processed by the shell, the most recent file path will be used if none is supplied.
