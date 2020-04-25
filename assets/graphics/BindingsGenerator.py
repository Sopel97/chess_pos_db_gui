from HeaderFile import *
from SourceFile import *
from WrapperBuilder import *
from WheelGenerator import *
from cffi import FFI
import os
import shutil

"""
Pairs header and source files if possible.
Unpaired ones are returned separately.
returns a 3-tuple with:
    [0] - a list of pairs of header and source files (a tuple - first header then source)
    [1] - a list of lone header files
    [2] - a list of lone source files
"""
def _pairs_and_remainders(headers, sources):
    pairs = []
    lone_headers = []
    lone_sources = []

    # TODO: possibly optimize, currently O(n^2)
    for header in headers:
        for source in sources:
            if header.filepath.stem == source.filepath.stem:
                pairs.append((header, source))
                break
        else:
            lone_headers.append(header)

    for source in sources:
        for header in headers:
            if header.filepath.stem == source.filepath.stem:
                break
        else:
            lone_sources.append(source)

    return (pairs, lone_headers, lone_sources)

"""
Class used to generate bindings for each source file using cffi library

Attributes
----------
args : Namespace
    Arguments parsed by argparse's ArgumentParser
"""
class BindingsGenerator:

    def __init__(self, args):
        self.args = args

    """
    Generates bindings and wrapper for each pair of
    source and header files
    """
    def _generate_bindings_for_pairs(self, pairs):
        verbosity = self.args.verbose

        if len(pairs) == 0:
            if verbosity:
                print(f'No header source pairs to process')
            return

        for header, source in pairs:
            name = header.filepath.stem

            if verbosity:
                print(f'Compiling and creating bindings for {name}')

            ffibuilder = FFI()
            ffibuilder.cdef(header.declarations)
            ffibuilder.set_source('_' + name, '\n'.join(source.includes), sources=[source.filepath],
                                  include_dirs=self.args.include, libraries=self.args.library,
                                  library_dirs=self.args.lib_dir)
            ffibuilder.compile(verbose=verbosity)
            build_wrapper_for_headers(name, name, [header]);

    """
    Generates bindings and wrapper the remainder of files
    (ones that couldn't be paired)
    """
    def _generate_bindings_for_remainder(self, headers, sources):
        verbosity = self.args.verbose

        if len(headers) == 0 or len(sources) == 0:
            if verbosity:
                print(f'No remainder to process')
            return

        if len(headers) == 0:
            print(f'warning: remainder sources but no headers. The functions will be inaccessible.')

        if verbosity:
            print(f'Compiling and creating bindings for the unpaired files')

        includes = set()

        ffibuilder = FFI()

        for header in headers:
            ffibuilder.cdef(header.declarations, override=True)
            build_wrapper_for_headers(header.filepath.stem, '_remainder', [header])

        includes = set()
        for source in sources:
            for include in source.includes:
                includes.add(include)

        ffibuilder.set_source('__remainder', '\n'.join(includes), sources=[source.filepath for source in sources],
                              include_dirs=self.args.include, libraries=self.args.library,
                              library_dirs=self.args.lib_dir)
        ffibuilder.compile(verbose=verbosity)

    """
    Generates bindings and wrapper for each source file found at path given in arguments
    and builds wheel out of created package
    """
    def generate_bindings(self):
        path = self.args.files_path
        verbosity = self.args.verbose

        headers = get_header_files(path)
        sources = get_source_files(path)

        if verbosity:
            print(f'Copying needed files to destination directory')

        self.copy_needed_files_to_output_dir(headers)
        self.copy_needed_files_to_output_dir(sources)

        pairs, lone_headers, lone_sources = _pairs_and_remainders(headers, sources)

        self._generate_bindings_for_pairs(pairs)
        self._generate_bindings_for_remainder(lone_headers, lone_sources)

        if verbosity:
            print('Cleaning up output dir before wheel generation')
        # Cleaning up a directory causes imports to fail in some test cases under linux
        # self.cleanup_output_dir()
        WheelGenerator('.', os.path.basename(self.args.files_path)).generate_wheel()

    """
    Copies all header or source files to output directory given in arguments

    Parameters
    ----------
    pairs : list
        List of header or source objects
    """
    def copy_needed_files_to_output_dir(self, files):
        for file in files:
            if not os.path.isfile('./' + file.filepath.name):
                shutil.copy2(str(file.filepath), '.')
            # TODO: copy needed library dependencies here too

    def cleanup_output_dir(self):
        """Cleans output directory leaving only .pyd and .py files"""

        for (root, dirs, files) in os.walk(self.args.dest, topdown=False):
            for file in files:
                if not (file.endswith('.py') or file.endswith('.pyd')):
                    os.remove(os.path.join(root, file))
            for dirname in dirs:
                os.rmdir(os.path.join(root, dirname))
